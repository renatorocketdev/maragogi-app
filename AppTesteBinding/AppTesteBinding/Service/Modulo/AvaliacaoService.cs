using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTesteBinding.Service.Modulo
{
    class AvaliacaoService : DataService
    {
        public async Task<bool> Save(AvaliacaoEmpresa avaliacao)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var uri = new Uri(string.Format("https://www.ocaribedemaragogi.com.br/api/APIAvaliacao/{0}", string.Empty));

                var content = new StringContent(JsonConvert.SerializeObject(avaliacao, Formatting.None), Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await httpClient.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", response.ReasonPhrase, "OK");
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Muito bom, recebemos sua avaliação!!", response.ReasonPhrase, "OK");
                    return false;
                }
            }
        }
    }
}
