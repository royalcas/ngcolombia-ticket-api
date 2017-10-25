using NGColombia.Api.Dto.Input;
using NGColombia.Api.Models;
using NGColombia.Api.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NGColombia.Api.Dto.Output;
using NGColombia.Api.Service.Exceptions;

namespace NGColombia.Api.Service
{
    public class TicketService : ITicketService
    {
        private NGColombiaDbContext context;
        private readonly IPaymentProvider paymentProvider;
        private readonly IEventService eventService;

        public TicketService(NGColombiaDbContext context, IPaymentProvider paymentProvider, IEventService eventService)
        {
            this.context = context;
            this.paymentProvider = paymentProvider;
            this.eventService = eventService;
        }

        public async Task<PaymentProviderForm> SaveInitialTransaction(TransactionInputModel inputTransaction)
        {
            await ValidateTransaction(inputTransaction);
            Transaction transaction = MapTransactionFromInput(inputTransaction);

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            context.Entry(transaction).State = EntityState.Detached;

            var savedTransaction = await context.Transactions
                .Include(transac => transac.Customer)
                .Include(transac => transac.Details)
                    .ThenInclude(detail => detail.TicketType)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == transaction.Id);

            savedTransaction.TotalValue = savedTransaction.GetTotalValue();

            var form = this.paymentProvider.GetPaymentForm(savedTransaction);

            savedTransaction.Signature = form.Signature;
            savedTransaction.IsTestTransaction = form.Test.Equals("1");
            context.Entry(savedTransaction).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return form;

        }

        private async Task ValidateTransaction(TransactionInputModel inputTransaction)
        {
            await ValidateAvailableTickets(inputTransaction);
            ValidateCustomerAlready(inputTransaction);
        }

        private async Task ValidateEventPurchaseDate()
        {
            var eventInformation = await context.Events.SingleOrDefaultAsync();
            if (DateTime.Now >= eventInformation.ConfirmationDateLimit)
            {
                throw new CheckoutUnavailableException($"We cannot reserve your ticket, the chekout limit was on {eventInformation.ConfirmationDateLimit}");
            }

        }
        private async Task ValidateAvailableTickets(TransactionInputModel inputTransaction)
        {
            var availableTickets = await this.eventService.GetAvailableTicketsSummary();
            var notAvaliableTickets = from ticket in availableTickets
                                      join currentPurchaseTicket in inputTransaction.Tickets
                                          on ticket.Code equals currentPurchaseTicket.TicketCode
                                      where ticket.AvailableTickets <= currentPurchaseTicket.Quantity
                                      select ticket;

            if (notAvaliableTickets.Any())
            {
                var notAvailableTicketNames = string.Join(", ", notAvaliableTickets.Select(ticket => ticket.Name));
                throw new CheckoutUnavailableException($"There are no available tickets for {notAvailableTicketNames}");
            }
        }

        private void ValidateCustomerAlready(TransactionInputModel inputTransaction)
        {
            var ticketsToBuy = inputTransaction.Tickets.Select(ticket => ticket.TicketCode);
            var alreadyApprobedTickets = context.TicketTypes
                .Include(ticket => ticket.Details)
                    .ThenInclude(detail => detail.Transaction)
                .Where(ticket => ticket.Details.Where(detail =>
                    detail.Transaction.Approved &&
                    detail.Transaction.Customer.IdentificationNumber.Equals(inputTransaction.CustomerId)).Any());

            if (alreadyApprobedTickets.Any())
            {
                var acquiredTickets = string.Join(", ", alreadyApprobedTickets.Select(ticket => ticket.Name).ToArray());
                throw new CheckoutUnavailableException($"The customer {inputTransaction.CustomerId} has already bought tickets for {acquiredTickets}, there is a limit of 1 per participant, please checkout the pending events separatelly");
            }
        }

        private static Transaction MapTransactionFromInput(TransactionInputModel inputTransaction)
        {
            var transaction = new Transaction();
            transaction.ConfirmationDate = DateTime.Now;
            transaction.Customer = new Customer();
            transaction.Customer.IdentificationNumber = inputTransaction.CustomerId;
            transaction.Customer.Email = inputTransaction.Email;
            transaction.Customer.Name = inputTransaction.CustomerName;
            transaction.Customer.PhoneNumber = inputTransaction.CustomerPhoneNumber;
            transaction.StartDate = DateTime.Now;
            

            var transactionDetails = new List<TransactionTicketDetail>();

            foreach (var detail in inputTransaction.Tickets)
            {
                transactionDetails.Add(new TransactionTicketDetail()
                {
                    TicketTypeCode = detail.TicketCode,
                    Quantity = detail.Quantity
                });
            }

            transaction.Details = transactionDetails;
            return transaction;
        }

        public async Task<TransactionResult> ProcessResponse(PayUResponse response)
        {
            var log = new PaymentResponseLog()
            {
                LapResponseCode = response.LapResponseCode,
                LapTransationState = response.LapTransactionState,
                PolResponseCode = response.PolResponseCode,
                PolTransactionState = response.PolTransactionState,
                ProcessDate = DateTime.Now,
                TransactionId = new Guid(response.ReferenceCode),
                TransactionState = response.TransactionState.ToString(),
                RawData = Newtonsoft.Json.JsonConvert.SerializeObject(response)
            };

            context.PaymentResponseLogs.Add(log);
            await context.SaveChangesAsync();
            
            var success = response.TransactionState == 4;

            var transactionResult = new TransactionResult()
            {
                Success = success,
                Pending = response.TransactionState == 7
            };

            if (success)
            {
                await ApproveTransaction(log.TransactionId);
            }
            return transactionResult;
        }

        public async Task<TransactionResult> Confirm(PayUConfirmation confirmation)
        {
            var log = new PaymentConfirmationLog()
            {
               
                ProcessDate = DateTime.Now,
                TransactionId = new Guid(confirmation.reference_sale),
                RawData = Newtonsoft.Json.JsonConvert.SerializeObject(confirmation),
                ReferencePayU = confirmation.reference_pol,
                ResponseCodePol = confirmation.response_code_pol,
                ResponseMessagePol = confirmation.response_message_pol,
                StatePol = confirmation.state_pol
            };

            context.PaymentConfirmationLogs.Add(log);
            await context.SaveChangesAsync();

            var success = confirmation.state_pol.Equals("4");

            var transactionResult = new TransactionResult()
            {
                Success = success
            };

            if (success)
            {
                await ApproveTransaction(log.TransactionId);
            }

            return transactionResult;
        }

        private async Task ApproveTransaction(Guid transactionId)
        {
            var transaction = await context.Transactions.SingleOrDefaultAsync(tran => tran.Id == transactionId);
            transaction.ConfirmationDate = DateTime.Now;
            transaction.Approved = true;
            context.Entry(transaction).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
