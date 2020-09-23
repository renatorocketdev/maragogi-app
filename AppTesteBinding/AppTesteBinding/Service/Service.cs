using AppTesteBinding.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTesteBinding.Service.Modulo
{
    public class Service<T> : DataService
    {
        public async Task<List<T>> Get(string api, string parameter = "", string arg = "")
        {
            using (var httpClient = new HttpClient())
            {
                Uri url = null;

                if (parameter == "")
                {
                    url = new DataService().BuilderUri(api);
                }
                else
                {
                    url = new DataService().BuilderUri(api, parameter, arg);
                }

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<T>>(json);
                }

                return new List<T>();
            }
        }

        public async Task<List<T>> GetByMulti(string api, Dictionary<string, string> keyValues)
        {
            using (var httpClient = new HttpClient())
            {
                Uri url = new DataService().BuilderUri(keyValues);

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<T>>(json);
                }

                return new List<T>();
            }
        }

        public async Task<ObservableCollection<T>> ObservableGet(string api, string parameter = "", string arg = "")
        {
            using (var httpClient = new HttpClient())
            {
                Uri url = null;

                if (parameter == "")
                {
                    url = new DataService().BuilderUri(api);
                }
                else
                {
                    url = new DataService().BuilderUri(api, parameter, arg);
                }

                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
                }

                return new ObservableCollection<T>();
            }
        }

        public async Task<HttpResponseMessage> AddAvaliation(AvaliacaoEmpresa avaliacao)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri(string.Format("https://www.ocaribedemaragogi.com.br/api/APIAvaliacao/{0}", string.Empty));

                var content = new StringContent(JsonConvert.SerializeObject(avaliacao, Formatting.None), Encoding.UTF8, "application/json");

                return await httpClient.PostAsync(uri, content);
            }
        }

        public async Task<HttpResponseMessage> AddFotoComentario(FotoComentario fotoComentario)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri(string.Format("https://www.ocaribedemaragogi.com.br/api/APIFotoComentario/{0}", string.Empty));

                var content = new StringContent(JsonConvert.SerializeObject(fotoComentario, Formatting.None), Encoding.UTF8, "application/json");

                return await httpClient.PostAsync(uri, content);
            }
        }
    }
}
