using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Books.Front.Services
{
    public static class ApiClient
    {
        public static HttpClient Create()
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
