using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HttpClients
{
    public abstract class BaseClient
    {
        public HttpClient Client { get; set; }
        protected abstract string ServiceAddress { get; }

        protected BaseClient(string baseAddress)
        {

            Client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                
            };

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


        }

        protected HttpResponseMessage Delete(string url)
        {
            return DeleteAsync(url).Result;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await this.Client.GetAsync(url);

        }

        protected T Get<T>(string url) where T : new()
        {
            return GetAsync<T>(url).Result;
        }

        protected async Task<T> GetAsync<T>(string url) where T : new()
        {
            var list = new T();
            var response = await this.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<T>();
            }

            return list;
        }

        protected HttpResponseMessage Post<T>(string url, T value)
        {
            return PostAsync<T>(url, value).Result;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T value)
        {
            var response = await Client.PostAsJsonAsync(url, value);

            return response;
        }

        protected HttpResponseMessage Put<T>(string url, T value)
        {
            return PutAsync<T>(url, value).Result;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T value)
        {
            var response = await Client.PutAsJsonAsync(url, value);

            return response;
        }
    }
}
