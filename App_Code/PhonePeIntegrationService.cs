using System;
using System.Net;
using System.Net.Http;

using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Net.Http.Headers;
/// <summary>
/// Summary description for PhonePeIntegrationService
/// </summary>
public class PhonePeIntegrationService
{
    private const string BaseUrl = "https://api-preprod.phonepe.com/apis/pg-sandbox";
    private const string SaltKey = "099eb0cd-02cf-4e2a-8aca-3e6c6aff0399";
    private const int SaltIndex = 1;

    private HttpClient httpClient;

    public PhonePeIntegrationService()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public string GetPaymentInitiationResponse(PaymentRequest paymentRequest)
    {
        string payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(paymentRequest);
        string base64EncodedPayload = Base64Encode(payloadJson);

        string xVerify = ComputeSha256Hash(base64EncodedPayload)+"###1";





        ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 |
 (SecurityProtocolType)768 | (SecurityProtocolType)3072;


        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, BaseUrl+ "/pg/v1/pay");
        requestMessage.Content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
        requestMessage.Headers.Add("X-VERIFY", xVerify);

        // Send the request and get the response synchronously
      
        HttpResponseMessage response = httpClient.SendAsync(requestMessage).Result;

        // Check if the request was successful
        if (response.IsSuccessStatusCode)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
        else
        {
            // Handle error
            string errorMessage = response.Content.ReadAsStringAsync().Result;
            throw new Exception(errorMessage);
        }        
    }

    private string Base64Encode(string plainText)
    {
         
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static String sha256_hash(string base64EncodedPayload)
    {
        StringBuilder Sb = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(base64EncodedPayload+ "/pg/v1/pay"+SaltKey));

            foreach (byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }


    static string ComputeSha256Hash(string base64EncodedPayload)
    {
        // Create a SHA256
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(base64EncodedPayload+ "/pg/v1/pay" + SaltKey));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    private string CalculateXVerifyHeader(string base64EncodedPayload)
    {
        string combinedString = base64EncodedPayload + "/pg/v1/pay";
        byte[] combinedBytes = Encoding.UTF8.GetBytes(combinedString);

        using (var sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(combinedBytes);

            string hashedString = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return hashedString + "###" + SaltIndex.ToString();
        }
    }
}