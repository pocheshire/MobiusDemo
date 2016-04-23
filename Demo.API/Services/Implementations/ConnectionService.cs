using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Demo.API.Services.Implementations
{
    internal static class ConnectionService
    {
        public static async Task<T> Get<T>(string url, string errorMessage = null)
        {
            return await ProcessRequest<T>(url, null, errorMessage);
        }

        public static async Task<T> Post<T>(string url, HttpContent postData, string errorMessage = null)
        {
            return await ProcessRequest<T>(url, postData, errorMessage);
        }

        private static async Task<T> ProcessRequest<T>(string url, HttpContent postData, string errorMessage)
        {
            using (var handler = new HttpClientHandler())
            {
                if (handler.SupportsAutomaticDecompression)
                    handler.AutomaticDecompression = DecompressionMethods.Deflate;

                using (var httpClient = new HttpClient(handler))
                {

                    using (var message = new HttpRequestMessage())
                    {
                        message.RequestUri = new Uri(url);

                        message.Method = postData == null ? HttpMethod.Get : HttpMethod.Post;

                        if (postData != null)
                            message.Content = postData;
                        
                        string data;
                        try
                        {
                            HttpResponseMessage response = await httpClient.SendAsync(message, CancellationToken.None).ConfigureAwait(false);
                            data = await response.Content.ReadAsStringAsync();

                            #if DEBUG && __MonoCS__
                            string formattedOutput = string.Empty;
                            formattedOutput += "#### Request " + message.Method + "####" + Environment.NewLine;
                            formattedOutput += "URL: " + url + Environment.NewLine;


                            if (postData != null)
                            {
                                string json = JsonConvert.SerializeObject(postData);
                                formattedOutput += "Post: " + json + Environment.NewLine;
                            }

                            formattedOutput += "Response length: " + data.Length + Environment.NewLine + "#### Request ####" + Environment.NewLine + Environment.NewLine;
                            formattedOutput += "#### Response " + message.Method + "####" + Environment.NewLine;
                            formattedOutput += data + Environment.NewLine;
                            formattedOutput += "#### End Response ####" + Environment.NewLine;
                            Debug.WriteLine(formattedOutput);

                            #endif

                        }
                        catch (Exception e)
                        {
                            throw new Exception(errorMessage, e);
                        }

                        if (!string.IsNullOrEmpty(data))
                            return JsonConvert.DeserializeObject<T>(data);
                        else
                            throw new Exception(errorMessage);
                    }  
                }
            }

            throw new Exception(errorMessage);
        }
    }
}