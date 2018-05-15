using Mowards.Models;
using Mowards.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.MowardsService
{
    internal class TMDBHttp
    {
        // Creating an instance of the http client
        private static HttpClient httpClient = new HttpClient();

        //private void SetHeaders()
        //{
        //    httpClient.DefaultRequestHeaders.Clear();
        //    httpClient.DefaultRequestHeaders.Add("api-key", "9727dec007ba17df175696cbb6e051bc");
           
        //}
        internal async Task<T> Get<T>(string url) where T : class
        {
            //SetHeaders();
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            HttpResponseMessage response = await httpClient.GetAsync(url);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response);
            }
            var rawJson = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(rawJson, settings);
            return result;
        }
        private async Task HandleError(HttpResponseMessage response)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new LowercaseContractResolver()
            };
            
            if (response.StatusCode
              == HttpStatusCode.Forbidden)
            {
                throw new SecurityException("Forbidden");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            WebAPIException logData =
                JsonConvert.DeserializeObject<WebAPIException>(responseJson, settings);
            throw new Exception(logData.Details);
        }
    }
}
