namespace NGColombia.Api.Service
{
    using NGColombia.Api.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dto.Output;
    using System.Collections;
    using Microsoft.EntityFrameworkCore;
    using NGColombia.Api.Models;
    using Newtonsoft.Json;

    public class EventService : IEventService
    {
        private NGColombiaDbContext context;
        private readonly IPaymentProvider paymentProvider;

        public EventService(NGColombiaDbContext context, IPaymentProvider paymentProvider)
        {
            this.context = context;
            this.paymentProvider = paymentProvider;
        }

        public async Task<IEnumerable<AvailableTicketInformation>> GetAvailableTicketsSummary() {
            var ticketTypes = await context.TicketTypes
                                .Include(type => type.Details)
                                    .ThenInclude(detail => detail.Transaction)
                                    .ThenInclude(process => process.Responses)
                                    .ToListAsync();

            return MapToOutput(ticketTypes);
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            var transactions = await context.Transactions
                                .Include(type => type.Details)
                                .Include(transaction => transaction.Responses)
                                .Include(transaction => transaction.Confirmations)
                                .Include(transaction => transaction.Customer)
                                .Where(tran => !tran.IsTestTransaction)
                                .ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsPendingForConfirmation()
        {
            var transactions = await context.Transactions
                                .Include(type => type.Details)
                                .Include(transaction => transaction.Responses)
                                .Include(transaction => transaction.Confirmations)
                                .Include(transaction => transaction.Customer)
                                .Where(tran => !tran.IsTestTransaction && !tran.Approved && !tran.Closed)
                                .ToListAsync();

            return transactions;
        }

        public async Task<int> ConfirmPendingTransactions()
        {
            var pendingTransactions = await this.GetTransactionsPendingForConfirmation();

            foreach (var transaction in pendingTransactions)
            {
                await this.ConfirmPendingTransaction(transaction);
            }

            await this.context.SaveChangesAsync();

            return pendingTransactions.Count();
        }

        private async Task ConfirmPendingTransaction(Transaction transaction)
        {
            var orderInfo = await this.paymentProvider.GetOrderByReferenceId(transaction.Id.ToString("D"));
            if (orderInfo.result == null || orderInfo.result.payload == null || !orderInfo.result.payload.Any() || orderInfo.result.payload.First().transactions == null)
                return;

            try
            {
                var payUResult = orderInfo.result.payload.First().transactions.First();
                var confirmation = new PaymentConfirmationLog();
                confirmation.TransactionId = transaction.Id;
                confirmation.ProcessDate = DateTime.Now;
                confirmation.ReferencePayU = payUResult.id;
                confirmation.RawData = JsonConvert.SerializeObject(orderInfo);
                confirmation.ResponseMessagePol = payUResult.transactionResponse.state;
                context.Entry(transaction).State = EntityState.Modified;
                var confirmations = new List<PaymentConfirmationLog>();
                confirmations.Add(confirmation);
                transaction.Confirmations = confirmations;
                transaction.Closed = true;
                transaction.Status = confirmation.ResponseMessagePol;
                transaction.Approved = string.Equals("APPROVED", payUResult.transactionResponse.state);
            }
            catch
            {

            }

        }

        private IEnumerable<AvailableTicketInformation> MapToOutput(IEnumerable<TicketType> types)
        {
            return types.Select(type => new AvailableTicketInformation()
            {
                Code = type.Code,
                Name = type.Name,
                AvailableTickets = type.GetAvailableTicketsQuantity()
            });
        }
            
    }
}
