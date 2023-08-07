using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PaymentRequest
/// </summary>
public class PaymentRequest
{
    public string merchantId { get; set; }
    public string merchantTransactionId { get; set; }
    public string merchantUserId { get; set; }
    public long amount { get; set; }
    public string redirectUrl { get; set; }
    public string redirectMode { get; set; }
    public string callbackUrl { get; set; }
    public string mobileNumber { get; set; }
    public PaymentInstrument paymentInstrument { get; set; }

    public class PaymentInstrument
    {
        public string type { get; set; }
    }
}