using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Exaxxi.Helper
{
    public class CallAPI
    {
        public HttpClient Initial(string token = null)
        {
             var Client = new HttpClient();

            Client.BaseAddress = new Uri("http://localhost:51340");
            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return Client;
        }
        public async Task<bool> postAPI(object obj, string link, string token = null)
        {
            HttpClient client = this.Initial(token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync(link, stringContent);
            if (res.IsSuccessStatusCode)
            {
                return true;  
            }
            return false;
        }
        public async Task<string> getAPI(string link, string token = null)
        {
            HttpClient client = this.Initial(token);
            HttpResponseMessage res = await client.GetAsync(link);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }
        public async Task<bool> deleteAPI(string link, string token = null)
        {
            HttpClient client = this.Initial(token);
            HttpResponseMessage res = await client.DeleteAsync(link);
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> putAPI(object obj, string link, string token = null)
        {
            HttpClient client = this.Initial(token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PutAsync(link, stringContent);
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> existAPI(string link, string token = null)
        {
            HttpClient client = this.Initial(token);
            HttpResponseMessage res = await client.GetAsync(link);
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
