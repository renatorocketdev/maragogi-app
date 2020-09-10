using AppTesteBinding.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class EmpresaService : DataService
    {
        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFundoEmpresaPath(string nomeEmpresa, string subCategoria)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(this.ApiBaseAddress + $"APIFotoEmpresa?NomeEmpresa={nomeEmpresa}&SubCategoria={subCategoria}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        return JsonConvert.DeserializeObject<ObservableCollection<FotosEstabelecimentos>>(json);
                    }
                }

                return null;
            }
        }
    }
}