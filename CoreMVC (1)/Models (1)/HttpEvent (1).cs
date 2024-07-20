using System.Diagnostics.Contracts;

namespace CoreMVC.Models
{
    public class HttpEvent
    {

        public static HttpClient DefaultRequestWithoutHeaders(string endPointURL)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            HttpClient client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(endPointURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch(Exception ex)
            {
                Console.WriteLine("DefaultRequestWithoutHeaders" + ex.Message);
            }
            return client;
        }

        public static HttpClient DefaultRequestHeaders(string endPointURL,string appKey)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            HttpClient client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(endPointURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("App-Key", appKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DefaultRequestHeaders" + ex.Message);
            }
            return client;
        }

        public static HttpClient Method_Headers(string endPointURL, string appKey,string accesstoken)
        {
            HttpClient client = DefaultRequestHeaders(endPointURL, appKey);
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accesstoken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Method_Headers" + ex.Message);
            }
            return client;
        }

        public static HttpClient HeadersForAccessTokenGenerate(string endPointURL, string appKey, string clientId,string clientSecret)
        {
            HttpClient client = DefaultRequestHeaders(endPointURL, appKey);
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        $"{clientId}:{clientSecret}")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("HeadersForAccessTokenGenerate" + ex.Message);
            }
            return client;
        }

    }
}
