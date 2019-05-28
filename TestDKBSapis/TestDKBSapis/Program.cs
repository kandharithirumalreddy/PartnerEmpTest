using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestDKBSapis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program...");
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create("https://login.microsoftonline.com/d3d8e52c-b3c6-4d7a-9857-34c82389369c/oauth2/token");
            tokenRequest.Method = "POST";
            string postData = "grant_type=+client_credentials&resource=+3f79188f-f9af-4203-96c1-4bcd76fd4fbc&client_id=+3f79188f-f9af-4203-96c1-4bcd76fd4fbc&client_secret=+ZB-bc%3FvCKBRt5F8dY2WEz0%5DDMRfOFrX%2B";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postData);

            tokenRequest.ContentType = "application/x-www-form-urlencoded";

            tokenRequest.ContentLength = byte1.Length;
            Stream newStream = tokenRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);

            string jsonString = null;
            HttpWebResponse response = tokenRequest.GetResponse() as HttpWebResponse;
            using (Stream responseStream1 = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream1, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(jsonString))
            {
                AuthenticationResponse authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(jsonString);
                string accessToken = authenticationResponse.access_token;
                if (!string.IsNullOrEmpty(accessToken))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    try
                    {
                        //for (int i = 1; i <= 1; i++)
                        {
                            string data = "{ \"firstName\": \"TestName\"," +
                            "\"lastName\": \"Røgind Jørgensen\"," +
                            "\"jobTitle\": \"Direktør\"," +
                            "\"telePhoneNumber\": \"123456\"," +
                            "\"email\": \"ega@itsmcompany.net\"," +
                            "\"partner\": \"214\"," +
                            "\"mailGroup\": \"string\"," +
                            "\"peSharePointId\": \"testSpecialCharacters5\"," +
                            "\"createdOn\": \"2019-05-18T08:33:33.723Z\"," +
                            "\"createdBy\": \"System account\"," +
                            "\"lastModified\": \"2019-05-18T08:33:33.723Z\"," +
                            "\"lastModifiedBY\": \"Eimantas\"," +
                            "\"modifiedOn\": \"2019-05-18T08:33:33.723Z\"," +
                            "\"modifiedBY\": \"Eimantas\"," +
                            "\"emailNotification\": true," +
                            "\"smsNotification\": true," +
                            "\"identifier\": \"test\"," +
                            "\"deactivatedUser\": false }";

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://dkbs-api-dev.azurewebsites.net/api/partneremployee");
                            request.ContentType = "application/json; charset=utf-8";
                            request.Headers.Add("Authorization", "Bearer " + accessToken);
                            request.Method = "POST";
                            /////////////////////////////////////////////////////
                            //request.Headers.Add("cache-control", "no-cache");
                            ////request.Headers.Add("accept-encoding", "gzip, deflate");
                            //request.Headers.Add("Cache-Control", "no-cache");
                            //request.KeepAlive = true;
                            //request.Host = "dkbs-api-dev.azurewebsites.net";
                            //request.Accept = "*/*";
                            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;



                            byte[] byte2 = encoding.GetBytes(data);
                            request.ContentLength = byte2.Length;
                            Stream newStream2 = request.GetRequestStream();
                            newStream2.Write(byte2, 0, byte2.Length);

                            string responseString = null;
                            using (HttpWebResponse dataResponse = request.GetResponse() as HttpWebResponse)
                            {
                                StreamReader reader = new StreamReader(dataResponse.GetResponseStream());
                                responseString = reader.ReadToEnd();
                                Console.WriteLine("Post request result: " + responseString);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    Console.ReadLine();
                }
            }
        }
    }
    public class AuthenticationResponse
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }
    }
}
