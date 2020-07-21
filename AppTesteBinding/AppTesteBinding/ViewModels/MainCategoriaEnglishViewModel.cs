using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTesteBinding.ViewModels
{
    class MainCategoriaEnglishViewModel : Manager<CategoriaEnglish>
    {
        public MainCategoriaEnglishViewModel()
        {

        }

        public MainCategoriaEnglishViewModel(string Categoria)
        {
            if(Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync(Categoria);
                TraslateTextCategoria(Categoria);
                AddFromAPIAsync(Categoria);
            }
            else if(App.Database.HasCategoriesEnglish(Categoria))
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };

                AddFromDataBaseAsync(Categoria);
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };

                ListLocal = new List<CategoriaEnglish>();
            }
           
        }

        private async void AddImagesFromAPIAsync(string categoria)
        {
            FotoIsBusy = true;

            var result = await new CategoriaService().GetFundoCategoriaPath(categoria);

            Fotos = new ObservableCollection<FotosEstabelecimentos>(result);

            FotoIsBusy = false;
        }
        public async void AddFromDataBaseAsync(string Categoria)
        {
            var result = await App.Database.GetCategoriesEnglishList(Categoria);
            ListLocal = new List<CategoriaEnglish>(result);
        }

        public async void AddFromAPIAsync(string Categoria)
        {
            CategoriasIsBusy = true;

            App.Database.DeleteCategories(Categoria);

            List<CategoriaEnglish> cat = new List<CategoriaEnglish>();

            var res = await new CategoriaService().GetCategoriasMainCategorias(Categoria);
            var traslate = await TraslateList(res);

            for (int i = 0; i < res.Count; i++)
            {
                cat.Add(new CategoriaEnglish
                {
                    CategoriaId = res[i].CategoriaId,
                    Icone = res[i].Icone,
                    MainCategoria = res[i].MainCategoria,
                    SubCategoria = res[i].SubCategoria,
                    SubCategoriaTranslate = traslate[i]
                });
            }

            ListLocal = new List<CategoriaEnglish>(cat);

            App.Database.SaveCategoriesEnglish(ListLocal);

            CategoriasIsBusy = false;
        }

        public async Task<List<string>> TraslateList(List<Categoria> list)
        {
            var Url = TranslateService.TranslateStringBuilder(list);
            return await TranslateService.GetTranslatePtEnList(Url);
        }

        public async void TraslateTextCategoria(string Categoria)
        {
            var Url = TranslateService.TranslateStringBuilder(Categoria);

            this.Categoria = await TranslateService.GetTranslatePtEn(Url);
        }
    }
}
