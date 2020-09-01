using AppTesteBinding.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class CategoriaService : DataService
    {
        public async Task<List<Categoria>> GetCategorias()
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APICategorias/");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<Categoria>>(json);
                }

                return new List<Categoria>();
            }
        }

        public async Task<List<Categoria>> GetCategoriasMainCategorias(string MainCategoria)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APICategorias?MainCategoria=" + MainCategoria.Replace(" ", ""));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<Categoria>>(json);
                }

                return new List<Categoria>();
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFundoCategoriaPath(string Categoria)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoCategoria?FundoCategoria=" + Categoria.Replace(" ", "")).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                }

                return new ObservableCollection<FotosEstabelecimentos>();
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFundoSubCategoriaPath(string SubCategoria)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoCategoria?SubCategoria=" + SubCategoria.Replace(" ", "%20"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                }

                return new ObservableCollection<FotosEstabelecimentos>();
            }
        }
    }
}
