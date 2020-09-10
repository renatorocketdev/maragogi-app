using System;
using System.Net.Http;

namespace AppTesteBinding.Service
{
    public class DataService
    {
        public HttpClient HttpClient;

        public string ApiBaseAddress { get; set; } = "https://ocaribedemaragogi.com.br/api";

        public Uri BuilderUri(string uri, string parameter, string arg)
        {
            return new Uri($"{this.ApiBaseAddress}/{uri}?{parameter}={arg}");
        }

        public Uri BuilderUri(string uri)
        {
            return new Uri($"{this.ApiBaseAddress}/{uri}/");
        }

        public DataService()
        {
            HttpClient = CreateClient();
        }

        public DataService(string uri, string parameter, string arg)
        {
            HttpClient = CreateClient();
        }

        public HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            return httpClient;
        }
    }
}