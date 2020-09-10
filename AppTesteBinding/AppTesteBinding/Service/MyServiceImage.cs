using AppTesteBinding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTesteBinding.Service
{
    public class MyServiceImage
    {
        public async Task<ObservableCollection<FotosEstabelecimentos>> GetImages(string api, string parameter = "", string arg = "")
        {
            using (var httpClient = new HttpClient())
            {
                Uri url = null;

                if (parameter == "")
                {
                    url = new DataService().BuilderUri(api);
                }
                else
                {
                    url = new DataService().BuilderUri(api, parameter, arg);
                }

                var response = await httpClient.GetAsync(url);

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
