using AppTesteBinding.Models;
using AppTesteBinding.Service;
using AppTesteBinding.Service.Modulo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTesteBinding.ViewModels
{
    public class PraiasViewModel : Manager<CategoriaMaragogi>
    {
        #region Constructors

        public PraiasViewModel()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync();

                AddFromAPIAsync();
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };
            }
        }

        #endregion Constructors

        #region Methods

        private async void AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            ListLocal = await new Service<CategoriaMaragogi>().Get("APICategoriasMaragogi", "Categoria", "Praias");

            CategoriasIsBusy = false;
        }

        private async void AddImagesFromAPIAsync()
        {
            FotoIsBusy = true;

            Fotos = await new MyServiceImage().GetImages("APIFotoCategoriasMaragogi", "tipo", "FundoPraias");

            FotoIsBusy = false;
        }

        #endregion Methods
    }
}