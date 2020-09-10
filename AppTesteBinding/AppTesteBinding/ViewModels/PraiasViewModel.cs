using AppTesteBinding.Models;
using AppTesteBinding.Service;
using AppTesteBinding.Service.Modulo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTesteBinding.ViewModels
{
    public class PraiasViewModel : Manager<Praias>
    {
        #region Constructors

        public PraiasViewModel()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync();

                AddFromAPIAsync();
            }
            else if (App.Database.HasPraias())
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };

                AddFromDataBaseAsync();
            }
            else
            {
                if (ListLocal.Count == 0)
                    HasNoEnterprises = true;
            }
        }

        #endregion Constructors

        #region Methods

        private async void AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            App.Database.DeletePraias();

            ListLocal = await new Service<Praias>().Get("APIPraias");

            App.Database.SavePraias(ListLocal);

            CategoriasIsBusy = false;
        }

        private async void AddFromDataBaseAsync()
        {
            ListLocal = new List<Praias>(await App.Database.GetPraiasList());
        }

        private async void AddImagesFromAPIAsync()
        {
            FotoIsBusy = true;

            Fotos = await new MyServiceImage().GetImages("APIFotoPraias");

            FotoIsBusy = false;
        }

        #endregion Methods
    }
}