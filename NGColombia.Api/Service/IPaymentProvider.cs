using NGColombia.Api.Dto.Input.PayUContracts;
using NGColombia.Api.Dto.Output;
using NGColombia.Api.Models;
using System.Threading.Tasks;

namespace NGColombia.Api.Service
{
    public interface IPaymentProvider
    {
        PaymentProviderForm GetPaymentForm(Transaction transaction);
        Task<TransactionResultPayU> GetTransactionDetail(string transactionId);
        Task<OrderDetailByReferenceIdResponse> GetOrderByReferenceId(string transactionId);
        string GetSignature(string merchantId, string referenceId, double value, string currency);
        string GetSignature(string merchantId, string referenceId, double value, string currency, string statePol);
    }
}