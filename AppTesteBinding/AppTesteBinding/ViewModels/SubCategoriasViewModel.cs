using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace AppTesteBinding.ViewModels
{
    internal class SubCategoriasViewModel : Manager<Empresa>
    {
        #region Constructors

        public SubCategoriasViewModel()
        {
        }

        public SubCategoriasViewModel(string categoria)
        {
            HasNoEnterprises = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync(categoria);

                AddFromAPIAsync(categoria);

                if (Settings.Ingles)
                {
                    GetNameCategories(categoria);
                }
                else
                {
                    Categoria = categoria;
                }
            }
            else if (App.Database.HasEnterprises(categoria))
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };

                AddFromDataBaseAsync(categoria);

                Categoria = categoria;
            }
        }

        #endregion Constructors

        #region Methods

        public async void AddFromAPIAsync(string Categoria)
        {
            CategoriasIsBusy = true;

            App.Database.DeleteCategories(Categoria);

            var result = await new EmpresaService().GetEnterprises(Categoria);

            ListLocal = new List<Empresa>(result.Where(x => x.SubCategoria == Categoria));

            App.Database.SaveEnterprises(ListLocal);

            CategoriasIsBusy = false;
        }

        public async void AddFromDataBaseAsync(string Categoria)
        {
            ListLocal = await App.Database.GetEnterpriseList(Categoria);
        }

        public async void GetNameCategories(string categoria)
        {
            var Url = TranslateService.TranslateStringBuilder(categoria);
            Categoria = await TranslateService.GetTranslatePtEn(Url);
        }

        private async void AddImagesFromAPIAsync(string categoria)
        {
            FotoIsBusy = true;

            Fotos = await new CategoriaService().GetFundoSubCategoriaPath(categoria);

            FotoIsBusy = false;
        }

        #endregion Methods
    }
}