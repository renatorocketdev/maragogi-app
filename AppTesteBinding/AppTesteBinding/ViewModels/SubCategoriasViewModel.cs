namespace AppTesteBinding.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using AppTesteBinding.Models;
    using AppTesteBinding.Service;
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
                _ = AddImagesFromAPIAsync(categoria);

                _ = AddFromAPIAsync(categoria);

                if (Settings.Ingles)
                {
                    _ = GetNameCategories(categoria);
                }
                else
                {
                    Categoria = categoria;
                }
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" },
                };

                Categoria = categoria;
            }
        }

        /// <summary>
        /// Set the rating for enterprises <see cref="SubCategoriasViewModel"/> class.
        /// </summary>
        private void SetRating()
        {
            foreach (var item in this.ListLocal)
            {
                item.Media = Math.Round((item.QtdVotos == 0 || item.Nota == 0) ? 0 : Convert.ToDouble(item.Nota / item.QtdVotos), 1);

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
        public async Task AddFromAPIAsync(string categoria)
        {
            CategoriasIsBusy = true;

            var result = await new Service<Empresa>().Get("APIEmpresa", "SubCategoria", categoria);

            HasNoEnterprises = result.Count == 0;

            ListLocal = new List<Empresa>(result.OrderByDescending(x => x.Nota).ToList());

            SetRating();

            CategoriasIsBusy = false;
        }

        public async Task GetNameCategories(string categoria)
        {
            var Url = TranslateService.TranslateStringBuilder(categoria);
            Categoria = await TranslateService.GetTranslatePtEn(Url);
        }

        private async Task AddImagesFromAPIAsync(string categoria)
        {
            FotoIsBusy = true;

            Fotos = await new MyServiceImage().GetImages("APIFotoCategoria", "SubCategoria", categoria);

            FotoIsBusy = false;
        }
    }
}