using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Input.PayUContracts
{
    public class Merchant
    {
        public string apiLogin { get; set; }
        public string apiKey { get; set; }
    }

    public class PayUQuery<TQuery>
    {
        public bool test { get; set; }
        public string language { get; set; }
        public string command { get; set; }
        public Merchant merchant { get; set; }
        public TQuery details { get; set; }

        public PayUQuery(string apiLogin, string apiKey)
        {
            language = "en";
            test = false;
            this.merchant = new Merchant()
            {
                apiKey = apiKey,
                apiLogin = apiLogin
            };
        }
    }
    public class DetailsReferenceId
    {
        public string referenceCode { get; set; }
    }

    public class DetailsTransactionId
    {
        public string transactionId { get; set; }
    }

    public class PayUQueryOrderByReferenceId: PayUQuery<DetailsReferenceId>
    {
        public PayUQueryOrderByReferenceId(string apiLogin, string apiKey, string referenceId)
            :base(apiLogin, apiKey)
        {
            this.command = "ORDER_DETAIL_BY_REFERENCE_CODE";
            this.details = new DetailsReferenceId() { referenceCode = referenceId };
        }
    }

    public class PayUQueryOrderByTransactionId : PayUQuery<DetailsTransactionId>
    {
        public PayUQueryOrderByTransactionId(string apiLogin, string apiKey, string transactionId)
            : base(apiLogin, apiKey)
        {
            this.command = "TRANSACTION_RESPONSE_DETAIL";
            this.details = new DetailsTransactionId() { transactionId = transactionId };
        }
    }
}
