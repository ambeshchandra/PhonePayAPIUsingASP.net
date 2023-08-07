using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Policy;


public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public async Task senddataAsync()
    {
        try
        {





            string apiUrl = "https://api-preprod.phonepe.com/apis/pg-sandbox/pg/v1/pay";
            var input = new
            {
                merchantId = "MERCHANTUAT",
                merchantTransactionId = "MT7850590068188104",
                merchantUserId = "MUID123",
                amount = 10000,
                redirectUrl = "https://webhook.site/redirect-url",
                redirectMode = "POST",
                callbackUrl = "https://webhook.site/redirect-url",
                mobileNumber = "9999999999",
                // paymentInstrument = "'{type: PAY_PAGE }'"
                paymentInstrument = new PaymentRequest.PaymentInstrument { type = "PAY_PAGE" }

            };

            PaymentRequest paymentRequest = new PaymentRequest
            {
                merchantId = "AGMINFOONLINE",
                merchantTransactionId = "AGMT234256789",
                merchantUserId = "AGM123202322",
                amount = 1000,
                redirectUrl = "https://agminfotech.in/redirect-url",
                redirectMode = "POST",
                callbackUrl = "https://agminfotech.in/redirect-url",
                mobileNumber = "9433558585",
                paymentInstrument = new PaymentRequest.PaymentInstrument { type = "PAY_PAGE" }
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 |
(SecurityProtocolType)768 | (SecurityProtocolType)3072;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";

            byte[] bytes = Encoding.UTF8.GetBytes("[" + paymentRequest + "]");
            using (Stream stream = httpRequest.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }

            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string json = (new StreamReader(stream)).ReadToEnd();
                    // obj.excuteSql("insert into BC_Initiate (Userid,Ack,IDate,ip) values ('" + Session["userid"].ToString() + "','" + json.ToString() + "',SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'),'" + obj.getIPAdd() + "')");
                    DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(json);
                    if (dt1.Rows.Count > 0)
                    {
                        DataColumnCollection columns = dt1.Columns;
                        if (columns.Contains("StatusCode"))
                        {
                            if (dt1.Rows[0]["StatusCode"].ToString().Trim() == "001")
                            {

                                Response.Redirect("https://icici.bankmitra.org/Location.aspx?text=" + dt1.Rows[0]["Result"].ToString().Trim());
                            }
                            else
                            {
                                // lblstatusmsg.Text = "Due to technical problem please try again !";
                            }
                        }
                    }
                    else
                    {
                        // lblstatusmsg.Text = "Due to technical problem please try again !";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "An error occurred: " + ex.Message.ToString();
        }
        finally
        {
        }
    }



    public void doddd()
    {
        string jsonreturn = "";
        string json;



        PaymentRequest paymentRequest = new PaymentRequest
        {
            merchantId = "MERCHANTUAT",
            merchantTransactionId = "MT7850590068188104",
            merchantUserId = "MUID123",
            amount = 10000,
            redirectUrl = "https://webhook.site/redirect-url",
            redirectMode = "POST",
            callbackUrl = "https://webhook.site/redirect-url",
            mobileNumber = "9999999999",
            paymentInstrument = new PaymentRequest.PaymentInstrument { type = "PAY_PAGE" }
        };



        string apiUrl = "https://api-preprod.phonepe.com/apis/pg-sandbox/pg/v1/pay";
        var input = new
        {

            merchantId = "MERCHANTUAT",
            merchantTransactionId = "MT7850590068188104",
            merchantUserId = "MUID123",
            amount = 10000,
            redirectUrl = "https://webhook.site/redirect-url",
            redirectMode = "POST",
            callbackUrl = "https://webhook.site/redirect-url",
            mobileNumber = "9999999999",
            // paymentInstrument= "{type: PAY_PAGE }"
            paymentInstrument = new PaymentRequest.PaymentInstrument { type = "PAY_PAGE" }

        };
        string inputJson = (new JavaScriptSerializer()).Serialize(input);

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 |
   (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
        httpRequest.ContentType = "application/json";
        httpRequest.Method = "POST";

        byte[] bytes = Encoding.UTF8.GetBytes("[" + paymentRequest + "]");
        using (Stream stream = httpRequest.GetRequestStream())
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        {
            using (Stream stream = httpResponse.GetResponseStream())
            {
                json = (new StreamReader(stream)).ReadToEnd();

            }
        }
        jsonreturn = json;
        JavaScriptSerializer jss = new JavaScriptSerializer();
        var details = jss.Deserialize<dynamic>(json);





        //var options = new RestClientOptions("https://api-preprod.phonepe.com/apis/pg-sandbox/pg/v1/pay");
        //var client = new RestClient(options);
        //var request = new RestRequest("");
        //request.AddHeader("accept", "application/json");
        //request.AddJsonBody("{\"request\":\"string\"}", false);
        //var response = await client.PostAsync(request);

        //Console.WriteLine("{0}", response.Content);

    }






    protected void paymentButton_ClickAsync()
    {
        // senddataAsync();
        // doddd();

        PhonePeIntegrationService phonePeService = new PhonePeIntegrationService();
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        try
        {
            // Prepare the payment request
            
            PaymentRequest paymentRequest = new PaymentRequest
            {
                merchantId = "MERCHANTUAT",
                merchantTransactionId = "MT7850590068188104",
                merchantUserId = "MUID123",
                amount = 10000,
                redirectUrl = "https://webhook.site/redirect-url",
                redirectMode = "POST",
                callbackUrl = "https://webhook.site/redirect-url",
                mobileNumber = "9999999999",
                paymentInstrument = new PaymentRequest.PaymentInstrument { type = "PAY_PAGE" }
            };





            string response = phonePeService.GetPaymentInitiationResponse(paymentRequest);

            // Process the response from PhonePe here

            // For simplicity, we'll just display the response on the page
            lblErrorMessage.Text = response;
        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log the error, display a user-friendly message, etc.)
            lblErrorMessage.Text = "An error occurred: " + ex.Message.ToString();
        }
    }

    protected void paymentButton_Click(object sender, EventArgs e)
    {
        //var jsonTask = testcAsync();

        paymentButton_ClickAsync();
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api-preprod.phonepe.com/apis/pg-sandbox/pg/v1/pay"),
            Headers =
    {
        { "accept", "application/json" },
        { "X-VERIFY", "998df972a7e654cec7c8e991537ee04e5f198c00a60b4d474be4cefb53dc9144###1" },
    },
            Content = new StringContent("{\"request\":\"eyJtZXJjaGFudElkIjoiTUVSQ0hBTlRVQVQiLCJtZXJjaGFudFRyYW5zYWN0aW9uSWQiOiJNVDc4NTA1OTAwNjgxODgxMDQiLCJtZXJjaGFudFVzZXJJZCI6Ik1VSUQxMjMiLCJhbW91bnQiOjEwMDAwLCJyZWRpcmVjdFVybCI6Imh0dHBzOi8vd2ViaG9vay5zaXRlL3JlZGlyZWN0LXVybCIsInJlZGlyZWN0TW9kZSI6IlBPU1QiLCJjYWxsYmFja1VybCI6Imh0dHBzOi8vd2ViaG9vay5zaXRlL3JlZGlyZWN0LXVybCIsIm1vYmlsZU51bWJlciI6Ijk5OTk5OTk5OTkiLCJwYXltZW50SW5zdHJ1bWVudCI6eyJ0eXBlIjoiUEFZX1BBR0UifX0=\"}")
            {
                Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
            }
        };


       // HttpClient clients = new HttpClient();
 


        HttpResponseMessage response = client.SendAsync(request).Result;
        var responseString = response.Content.ReadAsStringAsync();
    }
    //public static  async Task testcAsync()
    //{
    //    var client = new HttpClient();
    //    var request = new HttpRequestMessage
    //    {
    //        Method = HttpMethod.Post,
    //        RequestUri = new Uri("https://api-preprod.phonepe.com/apis/pg-sandbox/pg/v1/pay"),
    //        Headers =
    //{
    //    { "accept", "application/json" },
    //    { "X-VERIFY", "998df972a7e654cec7c8e991537ee04e5f198c00a60b4d474be4cefb53dc9144###1" },
    //},
    //        Content = new StringContent("{\"request\":\"eyJtZXJjaGFudElkIjoiTUVSQ0hBTlRVQVQiLCJtZXJjaGFudFRyYW5zYWN0aW9uSWQiOiJNVDc4NTA1OTAwNjgxODgxMDQiLCJtZXJjaGFudFVzZXJJZCI6Ik1VSUQxMjMiLCJhbW91bnQiOjEwMDAwLCJyZWRpcmVjdFVybCI6Imh0dHBzOi8vd2ViaG9vay5zaXRlL3JlZGlyZWN0LXVybCIsInJlZGlyZWN0TW9kZSI6IlBPU1QiLCJjYWxsYmFja1VybCI6Imh0dHBzOi8vd2ViaG9vay5zaXRlL3JlZGlyZWN0LXVybCIsIm1vYmlsZU51bWJlciI6Ijk5OTk5OTk5OTkiLCJwYXltZW50SW5zdHJ1bWVudCI6eyJ0eXBlIjoiUEFZX1BBR0UifX0=\"}")
    //        {
    //            Headers =
    //    {
    //        ContentType = new MediaTypeHeaderValue("application/json")
    //    }
    //        }
    //    };

    //    var response = HttpClient.SendAsync(request);
    //    var responseResult = response.Result;

    //    //using (var ddd = await client.SendAsync(request))
    //    //{
    //    //    ddd.EnsureSuccessStatusCode();
    //    //    var body = await ddd.Content.ReadAsStringAsync();
    //    //    Console.WriteLine(body);
    //    //}
    //}
}