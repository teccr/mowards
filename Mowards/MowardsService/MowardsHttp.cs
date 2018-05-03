using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Mowards.Models;
using Mowards.Services;
using Newtonsoft.Json;

namespace Mowards.MowardsService
{
    internal class MowardsHttp 
    {
        // Creating an instance of the http client due to performance with the .NET implementaiton of HTTPClient.
        private static HttpClient httpClient = new HttpClient();

        private void SetHeaders(bool isAnnonymous)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            if(!isAnnonymous)
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", App.Current.Properties["AppToken"].ToString());
            }
        }

        internal async Task<T> Get<T>(string url) where T : class
        {
            SetHeaders(false);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            HttpResponseMessage response = await httpClient.GetAsync(url);
            //response.EnsureSuccessStatusCode();
            if(!response.IsSuccessStatusCode)
            {
                await HandleError(response);
            }
            var rawJson = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(rawJson, settings);
            return result;
        }

        internal async Task<T> PostDetails<T, K>(string url, K argumentsObject, 
                                        bool isAnnonymous = false) where T : class 
                                                                   where K : class
        {
            SetHeaders(isAnnonymous);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            var rawJson = JsonConvert.SerializeObject(argumentsObject, settings);
            var content = new StringContent(rawJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await httpClient.PostAsync(url, content);

            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(responseJson, settings);
            return result;
        }

        internal async Task<T> Post<T>(string url, T rawObject) where T : class
        {
            SetHeaders(false);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            var rawJson = JsonConvert.SerializeObject(rawObject, settings);
            var content = new StringContent(rawJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = 
                await httpClient.PostAsync(url, content);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(responseJson, settings);
            return result;
        }

        internal async Task<T> Put<T>(string url, T rawObject) where T : class
        {
            SetHeaders(false);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            var rawJson = JsonConvert.SerializeObject(rawObject, settings);
            var content = new StringContent(rawJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await httpClient.PutAsync(url, content);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(responseJson, settings);
            return result;
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            if(response.StatusCode 
               == HttpStatusCode.Unauthorized)
            {
                throw new SecurityException("User was not authorize to use the web API.");
            }
            if (response.StatusCode
              == HttpStatusCode.Forbidden)
            {
                throw new SecurityException("Email already registered, request Reset password or use different email.");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            WebAPIException logData = 
                JsonConvert.DeserializeObject<WebAPIException>(responseJson, settings);
            throw new Exception(logData.Details);
        }
    }
}
