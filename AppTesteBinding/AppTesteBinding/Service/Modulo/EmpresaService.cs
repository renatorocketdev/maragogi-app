using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class EmpresaService : DataService
    {

        public async Task<List<Empresa>> GetEnterprises()
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIEmpresa").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<Empresa>>(json);
                }

                return null;
            }
        }
        public async Task<List<Empresa>> GetEnterprises(string MainCategoria)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIEmpresa?SubCategoria=" + MainCategoria);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<Empresa>>(json);
                }

                return null;
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFundoEmpresaPath(string NomeEmpresa)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "APIFotoEmpresa?NomeEmpresa=" + NomeEmpresa.Replace(" ", "")).ConfigureAwait(false);

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
