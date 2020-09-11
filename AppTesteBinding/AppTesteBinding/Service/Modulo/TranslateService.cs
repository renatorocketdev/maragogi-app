using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class TranslateService : DataService
    {
        public const string BaseUrlTranslate = "https://translate.yandex.net/api/v1.5/tr.json/translate?";
        public const string KeyTranslate = "key=trnsl.1.1.20200527T003415Z.0e8436a3b40cf96d.88e87a9ea0079350dacd3103b8f221693a294aec";

        public string TranslateStringBuilder(List<Categoria> CategoriasList)
        {
            string Url = BaseUrlTranslate + KeyTranslate;

            foreach (var item in CategoriasList)
                Url = Url + "&text=" + item.SubCategoria;

            return Url;
        }

        public string TranslateStringBuilder(string Categoria)
        {
            string Url = BaseUrlTranslate + KeyTranslate + "&text=" + Categoria;

            return Url;
        }

        public async Task<List<string>> GetTranslatePtEnList(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Url + $"&lang=pt-en");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ApiTranslate>(json).text;
                }

                return null;
            }
        }

        public async Task<string> GetTranslatePtEn(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Url + $"&lang=pt-en");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ApiTranslate>(json).text.FirstOrDefault();
                }

                return null;
            }
        }

        public async Task<string> GetTranslateEnPt(string Url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Url + $"&lang=en-pt");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ApiTranslate>(json).text.FirstOrDefault();
                }

                return null;
            }
        }
    }
}
