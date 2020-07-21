﻿using AppTesteBinding.Models;
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

            ListLocal = new List<Praias>(await new PraiasService().GetPraias());

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

            Fotos = await new PraiasService().GetFundo();

            FotoIsBusy = false;
        }

        #endregion Methods
    }
}