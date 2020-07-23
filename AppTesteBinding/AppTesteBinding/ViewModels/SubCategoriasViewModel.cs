namespace AppTesteBinding.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AppTesteBinding.Models;
    using AppTesteBinding.Service.Modulo;
    using AppTesteBinding.Utils;
    using Xamarin.Essentials;

    internal class SubCategoriasViewModel : Manager<Empresa>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SubCategoriasViewModel"/> class.
        /// </summary>
        public SubCategoriasViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubCategoriasViewModel"/> class.
        /// </summary>
        /// <param name="categoria"></param>
        public SubCategoriasViewModel(string categoria)
        {
            this.HasNoEnterprises = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                this.AddImagesFromAPIAsync(categoria);

                this.AddFromAPIAsync(categoria);

                if (Settings.Ingles)
                {
                    this.GetNameCategories(categoria);
                }
                else
                {
                    this.Categoria = categoria;
                }
            }
            else if (App.Database.HasEnterprises(categoria))
            {
                this.Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" },
                };

                this.AddFromDataBaseAsync(categoria);

                this.Categoria = categoria;
            }
        }

        /// <summary>
        /// Set the rating for enterprises <see cref="SubCategoriasViewModel"/> class.
        /// </summary>
        private void SetRating()
        {
            foreach (var item in this.ListLocal)
            {
                item.Media = (item.QtdVotos == 0 || item.Nota == 0) ? 0 : Convert.ToDouble(item.Nota / item.QtdVotos);

                if (item.Media == 0 && item.QtdVotos == 0)
                {
                    item.Rating = "none";
                }
                else if ((item.Media > 0 && item.Media < 5) && item.QtdVotos >= 1)
                {
                    item.Rating = "bad";
                }
                else if (item.Media >= 5)
                {
                    item.Rating = "good";
                }
            }
        }

        /// <summary>
        /// Get entreprises with subcategory <see cref="SubCategoriasViewModel"/> class.
        /// </summary>
        /// <param name="categoria">Nome da categoria.</param>
        public async void AddFromAPIAsync(string categoria)
        {
            this.CategoriasIsBusy = true;

            App.Database.DeleteCategories(categoria);

            var result = await new EmpresaService().GetEnterprises(categoria);

            this.HasNoEnterprises = result.Count == 0;

            this.ListLocal = new List<Empresa>(result.OrderByDescending(x => x.Nota).ToList());

            App.Database.SaveEnterprises(this.ListLocal);

            this.SetRating();

            this.CategoriasIsBusy = false;
        }

        public async void AddFromDataBaseAsync(string categoria)
        {
            this.ListLocal = await App.Database.GetEnterpriseList(categoria);
        }

        public async void GetNameCategories(string categoria)
        {
            var Url = this.TranslateService.TranslateStringBuilder(categoria);
            this.Categoria = await this.TranslateService.GetTranslatePtEn(Url);
        }

        private async void AddImagesFromAPIAsync(string categoria)
        {
            this.FotoIsBusy = true;

            this.Fotos = await new CategoriaService().GetFundoSubCategoriaPath(categoria);

            this.FotoIsBusy = false;
        }
    }
}