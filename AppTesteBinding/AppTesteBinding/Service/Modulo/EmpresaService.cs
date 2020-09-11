using AppTesteBinding.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class EmpresaService : DataService
    {
        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFundoEmpresaPath(string nomeEmpresa, string subCategoria)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + $"/APIFotoEmpresa?NomeEmpresa={nomeEmpresa}&SubCategoria={subCategoria}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                    }
                }

                return new ObservableCollection<FotosEstabelecimentos>();
            }
        }
    }
}