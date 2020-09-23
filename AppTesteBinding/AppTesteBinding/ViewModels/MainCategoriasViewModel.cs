using AppTesteBinding.Models;
using AppTesteBinding.Service;
using AppTesteBinding.Service.Modulo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTesteBinding.ViewModels
{
    public class MainCategoriasViewModel : Manager<Categoria>
    {
        public MainCategoriasViewModel()
        {
        }

        public MainCategoriasViewModel(string categoria)
        {
            this.Categoria = categoria;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                _ = AddImagesFromAPIAsync(categoria);

                _ = AddFromAPIAsync(categoria);
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };

                ListLocal = new List<Categoria>();
            }
        }

        private async Task AddImagesFromAPIAsync(string categoria)
        {
            FotoIsBusy = true;

            var result = await new MyServiceImage().GetImages("APIFotoCategoria", "FundoCategoria", categoria);

            Fotos = new ObservableCollection<FotosEstabelecimentos>(result);

            FotoIsBusy = false;
        }

        public async Task AddFromAPIAsync(string Categoria)
        {
            CategoriasIsBusy = true;

            List<Categoria> cat;

            var res = await new Service<Categoria>().Get("APICategorias");

            cat = new List<Categoria>(res);

            var result = cat.Where(x => x.MainCategoria == Categoria);

            ListLocal = new List<Categoria>(result);

            CategoriasIsBusy = false;
        }
    }
}