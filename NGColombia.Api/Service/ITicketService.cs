using System;
using NGColombia.Api.Dto.Input;
using System.Threading.Tasks;
using NGColombia.Api.Dto.Output;

namespace NGColombia.Api.Service
{
    public interface ITicketService
    {
        Task<PaymentProviderForm> SaveInitialTransaction(TransactionInputModel inputTransaction);
        Task<TransactionResult> ProcessResponse(PayUResponse model);
        Task<TransactionResult> Confirm(PayUConfirmation model);
    }
}