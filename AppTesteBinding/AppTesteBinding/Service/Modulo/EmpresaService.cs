using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTesteBinding.Service.Modulo
{
    public class EmpresaService : DataService
    {

        public async Task<List<Empresa>> GetEnterprises()
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(this.ApiBaseAddress + "APIEmpresa").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        return JsonConvert.DeserializeObject<List<Empresa>>(json);
                    }
                }

                return null;
            }
        }
        public async Task<List<Empresa>> GetEnterprises(string mainCategoria)
        {
            using (var httpClient = new DataService().HttpClient)
            {
                var response = await httpClient.GetAsync(this.ApiBaseAddress + "APIEmpresa?SubCategoria=" + mainCategoria);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        return JsonConvert.DeserializeObject<List<Empresa>>(json);
                    }
                }

                return null;
            }
        }

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
