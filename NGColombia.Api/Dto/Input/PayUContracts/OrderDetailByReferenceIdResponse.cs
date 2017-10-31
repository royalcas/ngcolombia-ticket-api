using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGColombia.Api.Dto.Input.PayUContracts
{
    public class ShippingAddress
    {
        public object street1 { get; set; }
        public object street2 { get; set; }
        public object city { get; set; }
        public object state { get; set; }
        public string country { get; set; }
        public object postalCode { get; set; }
        public object phone { get; set; }
    }

    public class Buyer
    {
        public object merchantBuyerId { get; set; }
        public object fullName { get; set; }
        public string emailAddress { get; set; }
        public object contactPhone { get; set; }
    }

    public class CreditCard
    {
        public string maskedNumber { get; set; }
        public string name { get; set; }
        public string issuerBank { get; set; }
    }

    public class TransactionResponse
    {
        public string state { get; set; }
        public string paymentNetworkResponseCode { get; set; }
        public object paymentNetworkResponseErrorMessage { get; set; }
        public string trazabilityCode { get; set; }
        public string authorizationCode { get; set; }
        public object pendingReason { get; set; }
        public string responseCode { get; set; }
        public object errorCode { get; set; }
        public object responseMessage { get; set; }
        public object transactionDate { get; set; }
        public object transactionTime { get; set; }
        public long? operationDate { get; set; }
        public object extraParameters { get; set; }
    }

    public class BillingAddress
    {
        public string street1 { get; set; }
        public object street2 { get; set; }
        public string city { get; set; }
        public object state { get; set; }
        public string country { get; set; }
        public object postalCode { get; set; }
        public object phone { get; set; }
    }

    public class Payer
    {
        public object merchantPayerId { get; set; }
        public string fullName { get; set; }
        public BillingAddress billingAddress { get; set; }
        public string emailAddress { get; set; }
        public string contactPhone { get; set; }
        public string dniNumber { get; set; }
    }

    public class TXADDITIONALVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERTOTALAMOUNT
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class TXVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMTAXRETURNBASE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class COMMISSIONVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMNETWORKVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class DPMERCHANTCOMMISSIONVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMADDITIONALVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERPRICINGVALUES
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMTAX
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERCOMMISSIONVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class TXTAXRETURNBASE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERINTERESTVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class DPMERCHANTINTERESTVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class DPMERCHANTTOTALINCOMEVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPURCHASEVALUE
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class TXTAX
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class AdditionalValues
    {
        public TXADDITIONALVALUE TX_ADDITIONAL_VALUE { get; set; }
        public PMPAYERTOTALAMOUNT PM_PAYER_TOTAL_AMOUNT { get; set; }
        public TXVALUE TX_VALUE { get; set; }
        public PMTAXRETURNBASE PM_TAX_RETURN_BASE { get; set; }
        public PMVALUE PM_VALUE { get; set; }
        public COMMISSIONVALUE COMMISSION_VALUE { get; set; }
        public PMNETWORKVALUE PM_NETWORK_VALUE { get; set; }
        public DPMERCHANTCOMMISSIONVALUE DP_MERCHANT_COMMISSION_VALUE { get; set; }
        public PMADDITIONALVALUE PM_ADDITIONAL_VALUE { get; set; }
        public PMPAYERPRICINGVALUES PM_PAYER_PRICING_VALUES { get; set; }
        public PMTAX PM_TAX { get; set; }
        public PMPAYERCOMMISSIONVALUE PM_PAYER_COMMISSION_VALUE { get; set; }
        public TXTAXRETURNBASE TX_TAX_RETURN_BASE { get; set; }
        public PMPAYERINTERESTVALUE PM_PAYER_INTEREST_VALUE { get; set; }
        public DPMERCHANTINTERESTVALUE DP_MERCHANT_INTEREST_VALUE { get; set; }
        public DPMERCHANTTOTALINCOMEVALUE DP_MERCHANT_TOTAL_INCOME_VALUE { get; set; }
        public PMPURCHASEVALUE PM_PURCHASE_VALUE { get; set; }
        public TXTAX TX_TAX { get; set; }
    }

    public class ExtraParameters
    {
        public string IS_PAYU_CLICK { get; set; }
        public string CREDIBANCO_CERTIFICATE_MERCHANT_NAME { get; set; }
        public string MERCHANT_PROFILE_ID { get; set; }
        public string CREDIBANCO_RESPONSE_ANSWER_MESSAGE { get; set; }
        public string MAX_SHIPPING_MERCHANT { get; set; }
        public string MIN_SHIPPING_PAYER { get; set; }
        public string CHECKOUT_VERSION { get; set; }
        public string CREDIBANCO_REFERENCE_CODE { get; set; }
        public string MAX_SHIPPING_PAYER { get; set; }
        public string MIN_SHIPPING_MERCHANT { get; set; }
        public string INSTALLMENTS_NUMBER { get; set; }
        public string PRICING_PROFILE_GROUP_ID { get; set; }
        public string CREDIBANCO_CERTIFICATE_AGENCY_CODE { get; set; }
        public string PERCENT_SHIPPING_MERCHANT { get; set; }
        public string CREDIBANCO_CERTIFICATE_TERMINAL_NUMBER { get; set; }
        public string CREDIBANCO_RESPONSE_AUTHORIZATION_CODE { get; set; }
        public string CREDIBANCO_RESPONSE_RECEIPT_NUMBER { get; set; }
        public string IS_REGISTER_PAYER { get; set; }
    }

    public class TransactionData
    {
        public string id { get; set; }
        public object order { get; set; }
        public CreditCard creditCard { get; set; }
        public object bankAccount { get; set; }
        public string type { get; set; }
        public object parentTransactionId { get; set; }
        public string paymentMethod { get; set; }
        public object source { get; set; }
        public string paymentCountry { get; set; }
        public TransactionResponse transactionResponse { get; set; }
        public string deviceSessionId { get; set; }
        public string ipAddress { get; set; }
        public string cookie { get; set; }
        public string userAgent { get; set; }
        public object expirationDate { get; set; }
        public Payer payer { get; set; }
        public AdditionalValues additionalValues { get; set; }
        public ExtraParameters extraParameters { get; set; }
    }

    public class TXADDITIONALVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class TXVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMTAXRETURNBASE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class COMMISSIONVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMNETWORKVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class DPMERCHANTCOMMISSIONVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMADDITIONALVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERPRICINGVALUES2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class TXTAXRETURNBASE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERCOMMISSIONVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMTAX2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPAYERINTERESTVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class DPMERCHANTINTERESTVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class PMPURCHASEVALUE2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class TXTAX2
    {
        public double value { get; set; }
        public string currency { get; set; }
    }

    public class AdditionalValues2
    {
        public TXADDITIONALVALUE2 TX_ADDITIONAL_VALUE { get; set; }
        public TXVALUE2 TX_VALUE { get; set; }
        public PMTAXRETURNBASE2 PM_TAX_RETURN_BASE { get; set; }
        public PMVALUE2 PM_VALUE { get; set; }
        public COMMISSIONVALUE2 COMMISSION_VALUE { get; set; }
        public PMNETWORKVALUE2 PM_NETWORK_VALUE { get; set; }
        public DPMERCHANTCOMMISSIONVALUE2 DP_MERCHANT_COMMISSION_VALUE { get; set; }
        public PMADDITIONALVALUE2 PM_ADDITIONAL_VALUE { get; set; }
        public PMPAYERPRICINGVALUES2 PM_PAYER_PRICING_VALUES { get; set; }
        public TXTAXRETURNBASE2 TX_TAX_RETURN_BASE { get; set; }
        public PMPAYERCOMMISSIONVALUE2 PM_PAYER_COMMISSION_VALUE { get; set; }
        public PMTAX2 PM_TAX { get; set; }
        public PMPAYERINTERESTVALUE2 PM_PAYER_INTEREST_VALUE { get; set; }
        public DPMERCHANTINTERESTVALUE2 DP_MERCHANT_INTEREST_VALUE { get; set; }
        public PMPURCHASEVALUE2 PM_PURCHASE_VALUE { get; set; }
        public TXTAX2 TX_TAX { get; set; }
    }

    public class Payload1
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public string status { get; set; }
        public string referenceCode { get; set; }
        public string description { get; set; }
        public object airlineCode { get; set; }
        public string language { get; set; }
        public string notifyUrl { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public Buyer buyer { get; set; }
        public object antifraudMerchantId { get; set; }
        public List<TransactionData> transactions { get; set; }
        public AdditionalValues2 additionalValues { get; set; }
    }

    public class Result
    {
        public List<Payload1> payload { get; set; }
    }

    public class OrderDetailByReferenceIdResponse
    {
        public string code { get; set; }
        public object error { get; set; }
        public Result result { get; set; }
    }
    
    
}
