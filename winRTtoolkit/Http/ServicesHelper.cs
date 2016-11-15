using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace winRTtoolkit.Http
{
    public class ServicesHelper
    {
        /// <summary>
        /// ServicesHelper.ServerAddress = string.Format("{0}{1}", ServerAddress, Version);
        /// </summary>
        public static string ServerAddress;

        public static HttpWebRequest GetWebRequest(string endpointWithParams)
        {
            return (HttpWebRequest)WebRequest.Create(GetWebUri(endpointWithParams));
        }

        internal static Uri GetWebUri(string endpointWithParams)
        {
            StringBuilder uri = new StringBuilder(ServerAddress);
            uri.Append(endpointWithParams);

            return new Uri(uri.ToString());
        }

        public static async Task<String> HttpRequest(RestRequestTypes requestType, HttpWebRequest httpWebRequest, object postData = null)
        {
            string result;

            if (requestType.Equals(RestRequestTypes.Get))
                result = await HttpGetRequest(httpWebRequest);
            else
                result = await HttpRestRequest(requestType, httpWebRequest, postData);

            return result;
        }

        internal static async Task<String> HttpGetRequest(HttpWebRequest httpWebRequest)
        {
            String received = null;

            try
            {
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, null)))
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(responseStream))
                        {
                            received = await sr.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return received;
        }

        internal static async Task<String> HttpRestRequest(RestRequestTypes restRequestTypes, HttpWebRequest httpWebRequest, object postData)
        {
            String received = null;

            try
            {
                httpWebRequest.Method = restRequestTypes.ToString().ToUpperInvariant();

                byte[] requestBody;
                var dataString = postData as string;
                if (dataString != null)
                {
                    //httpWebRequest.ContentType = "application/json";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    requestBody = Encoding.UTF8.GetBytes(dataString);
                }
                else
                {
                    requestBody = postData as byte[];
                }

                // ASYNC: using awaitable wrapper to get httpWebRequest stream
                using (var postStream = await httpWebRequest.GetRequestStreamAsync())
                {
                    // Write to the httpWebRequest stream.
                    // ASYNC: writing to the POST stream can be slow
                    if (requestBody != null)
                    {
                        await postStream.WriteAsync(requestBody, 0, requestBody.Length);
                    }
                }

                // ASYNC: using awaitable wrapper to get response
                HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                if (response != null)
                {
                    var reader = new StreamReader(response.GetResponseStream());

                    // ASYNC: using StreamReader's async method to read to end, in case
                    // the stream i slarge.
                    received = await reader.ReadToEndAsync();

                    if (string.IsNullOrEmpty(received))
                    {
                        return JsonConvert.SerializeObject(response);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return received;
        }
    }
}
