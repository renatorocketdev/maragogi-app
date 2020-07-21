using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    class CategoriasMaragogiService : DataService
    {
        public async Task<List<CategoriaMaragogi>> GetCategoriasMaragogi()
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APICategoriasMaragogi/");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<CategoriaMaragogi>>(json);
                }

                return new List<CategoriaMaragogi>();
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFotoFundo(string CategoriaMaragogi)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoCategoriasMaragogi?CategoriasMaragogi=" + CategoriaMaragogi);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                }

                return null;
            }
        }
    }
}
