using AppTesteBinding.Models;
using Newtonsoft.Json;
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

        public async Task<string> Post(T value, string api)
        {
            using (var httpClient = new HttpClient())
            {
                var url = new DataService().BuilderUri(api);

                var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);

                return response.ReasonPhrase;
            }
        }
    }
}