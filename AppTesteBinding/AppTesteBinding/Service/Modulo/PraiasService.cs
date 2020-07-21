using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class PraiasService : DataService
    {
        public async Task<List<Praias>> GetPraias()
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIPraias");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<Praias>>(json);
                }

                return new List<Praias>();
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFotoFundo(string Praia)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoPraias?Praia=" + Praia);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                }

                return new ObservableCollection<FotosEstabelecimentos>();
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFundo()
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoPraias");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                }

                return new ObservableCollection<FotosEstabelecimentos>();
            }
        }
    }
}
