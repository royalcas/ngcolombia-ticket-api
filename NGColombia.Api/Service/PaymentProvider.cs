using Microsoft.Extensions.Options;
using NGColombia.Api.Dto.Output;
using NGColombia.Api.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGColombia.Api.Models;
using System.Security.Cryptography;
using System.Text;
using NGColombia.Api.Dto.Input.PayUContracts;
using RestSharp;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace NGColombia.Api.Service
{
    public class PaymentProvider : IPaymentProvider
    {
        private readonly IOptions<PayUSettings> payUSettings;

        public PaymentProvider(IOptions<PayUSettings> payUSettings)
        {
            this.payUSettings = payUSettings;
        }

        public PaymentProviderForm GetPaymentForm(Transaction transaction)
        {
            var form = new PaymentProviderForm();
            form.ActionUrl = payUSettings.Value.GatewayUrl;
            form.MerchantId = payUSettings.Value.MerchantId;
            form.AccountId = payUSettings.Value.AccountId;
            form.ConfirmationUrl = payUSettings.Value.ConfirmationUrl;
            form.ResponseUrl = payUSettings.Value.ResposeUrl;
            form.Description = transaction.GetTransactionDescription();
            form.Amount = transaction.TotalValue;
            form.Tax = Math.Round(transaction.TotalValue * 0.19, 2);
            form.TaxReturnBase = Math.Round(form.Amount - form.Tax);
            form.Currency = payUSettings.Value.Currency;
            form.BuyerFullName = transaction.Customer?.Name;
            form.BuyerEmail = transaction.Customer?.Email;
            form.ReferenceCode = transaction.Id.ToString();
            form.Signature = this.GetSignature(transaction);
            form.Test = payUSettings.Value.Test;
            return form;

        }

        public string GetSignature(Transaction transaction)
        {
            return this.GetSignature(this.payUSettings.Value.MerchantId, transaction.Id.ToString(), transaction.TotalValue, payUSettings.Value.Currency);
        }

        public string GetSignature(string merchantId, string referenceId, double value, string currency)
        {
            var signature = $"{this.payUSettings.Value.ApiKey}~{this.payUSettings.Value.MerchantId}~{referenceId}~{value:0.0#}~{currency}";
            return MD5Hash(signature);
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        public string GetSignature(string merchantId, string referenceId, double value, string currency, string statePol)
        {
            var signature = $"{this.payUSettings.Value.ApiKey}~{this.payUSettings.Value.MerchantId}~{referenceId}~{value:0.0#}~{currency}~{statePol}";
            return MD5Hash(signature);
        }

        public async Task<OrderDetailByReferenceIdResponse> GetOrderByReferenceId(string transactionId)
        {
            var body = new PayUQueryOrderByReferenceId(payUSettings.Value.AppLogin, payUSettings.Value.ApiKey, transactionId);
            return await this.ExecutePayUQuery<PayUQueryOrderByReferenceId, OrderDetailByReferenceIdResponse>(body);
        }

        public async Task<TransactionResultPayU> GetTransactionDetail(string transactionId)
        {
            var body = new PayUQueryOrderByTransactionId(payUSettings.Value.AppLogin, payUSettings.Value.ApiKey, transactionId);
            return await this.ExecutePayUQuery<PayUQueryOrderByTransactionId, TransactionResultPayU>(body);
        }

        public async Task<TResponse> ExecutePayUQuery<TQuery, TResponse>(TQuery query)
        {
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            var client = new RestClient(payUSettings.Value.ApiUrl);
            var request = new RestRequest(string.Empty, Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(query);
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            IRestResponse response = (RestResponse)(await taskCompletion.Task);
            var responseObject = JsonConvert.DeserializeObject<TResponse>(response.Content);
            return responseObject;
        }
    }
}
