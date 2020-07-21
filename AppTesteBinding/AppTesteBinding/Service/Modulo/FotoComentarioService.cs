using System;
using AppTesteBinding.Models;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Forms;
using System.Collections.Generic;

namespace AppTesteBinding.Service.Modulo
{
    class FotoComentarioService : DataService
    {
        public async Task<List<FotoComentario>> Get(string NomeEmpresa)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoComentario?NomeEmpresa=" + NomeEmpresa.Replace(" ", "")).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<FotoComentario>>(json);
                }

                return null;
            }
        }
        public async Task Save(FotoComentario fotoComentario)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var uri = new Uri(string.Format("https://www.ocaribedemaragogi.com.br/api/APIFotoComentario/{0}", string.Empty));

                var content = new StringContent(JsonConvert.SerializeObject(fotoComentario, Formatting.None), Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await httpClient.PostAsync(uri, content);

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", response.ReasonPhrase, "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Foto Enviada com Sucesso!!", response.ReasonPhrase, "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }
    }
}
