using NGColombia.Api.Dto.Output;
using NGColombia.Api.Models;

namespace NGColombia.Api.Service
{
    public interface IPaymentProvider
    {
        PaymentProviderForm GetPaymentForm(Transaction transaction);
        string GetSignature(string merchantId, string referenceId, double value, string currency);
        string GetSignature(string merchantId, string referenceId, double value, string currency, string statePol);
    }
}