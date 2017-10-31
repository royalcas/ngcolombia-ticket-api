using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGColombia.Api.Dto.Input.PayUContracts
{
    [XmlRoot(ElementName = "payload")]
    public class Payload
    {
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "paymentNetworkResponseCode")]
        public string PaymentNetworkResponseCode { get; set; }
        [XmlElement(ElementName = "trazabilityCode")]
        public string TrazabilityCode { get; set; }
        [XmlElement(ElementName = "authorizationCode")]
        public string AuthorizationCode { get; set; }
        [XmlElement(ElementName = "responseCode")]
        public string ResponseCode { get; set; }
        [XmlElement(ElementName = "operationDate")]
        public string OperationDate { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [XmlRoot(ElementName = "result")]
    public class TransactionResultContent
    {
        [XmlElement(ElementName = "payload")]
        public Payload Payload { get; set; }
    }

    [XmlRoot(ElementName = "reportingResponse")]
    public class TransactionResultPayU
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
        [XmlElement(ElementName = "result")]
        public TransactionResultContent Result { get; set; }
    }
    
}
