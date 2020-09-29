using AppTesteBinding.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppTesteBinding.Service.Modulo
{
    public class HistoriaMaragogiService : DataService
    {
        public async Task<HistoriaMaragogi> GetHistoriaMaragogi()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "/APIHistoriaMaragogi");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(json))
                        return JsonConvert.DeserializeObject<List<HistoriaMaragogi>>(json).FirstOrDefault();
                }

                return new HistoriaMaragogi();
            }
        }

        public async Task<ObservableCollection<FotosEstabelecimentos>> GetFotoFundo()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "/APIFotoHistoriaMaragogi");

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
