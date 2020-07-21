using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
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

        public MainCategoriasViewModel(string Categoria)
        {
            this.Categoria = Categoria;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync(Categoria);
                AddFromAPIAsync(Categoria);
            }
            else if(App.Database.HasCategories(Categoria))
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

                ListLocal = new List<Categoria>();
            }
                

            //FotoIsBusy = CategoriasIsBusy = true;

            //if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //{
            //    if (!Settings.Ingles)
            //    {
            //        this.Categoria = Categoria;

            //        GetAsync(Categoria);

            //        English = false;
            //        CategoriasIsBusy = false;
            //        Portuguese = true;
            //    }
            //    else
            //    {
            //        GetEngAsync(Categoria);

            //        TraslateTextCategoria(Categoria);

            //        CategoriasIsBusy = false;
            //        Portuguese = false;
            //        English = true;
            //    }
            //}
            //else
            //{
            //    Fotos = new ObservableCollection<FotosEstabelecimentos>
            //    {
            //        new FotosEstabelecimentos { Foto = "fundooffline.png" }
            //    };

            //    FotoIsBusy = false;

            //    this.Categoria = Categoria;

            //    GetNoConnection(Categoria);

            //    CategoriasIsBusy = false;
            //    Portuguese = true;
            //    English = false;
            //}
        }

        private async void AddImagesFromAPIAsync(string categoria)
        {
            FotoIsBusy = true;

            var result = await new CategoriaService().GetFundoCategoriaPath(categoria);

            Fotos = new ObservableCollection<FotosEstabelecimentos>(result);

            FotoIsBusy = false;
        }

        public async void AddFromAPIAsync(string Categoria)
        {
            CategoriasIsBusy = true;

            App.Database.DeleteCategories(Categoria);

            List<Categoria> cat;

            var res = await new CategoriaService().GetCategorias();

            cat = new List<Categoria>(res);

            var result = cat.Where(x => x.MainCategoria == Categoria);

            ListLocal = new List<Categoria>(result);

            App.Database.SaveCategories(ListLocal);

            CategoriasIsBusy = false;
        }

        public async void AddFromDataBaseAsync(string Categoria)
        {
            var result = await App.Database.GetCategoriesList(Categoria);
            ListLocal = new List<Categoria>(result);
        }

        //public async void GetAsync(string Categoria)
        //{
        //    App.Database.DeleteCategories(Categoria);

        //    Persist.Categorias = await new CategoriaService().GetCategorias();

        //    var result = Persist.Categorias.Where(x => x.MainCategoria == Categoria);

        //    ListLocal = new List<Categoria>(result);

        //    App.Database.SaveCategories(ListLocal);
        //}

        //public async void GetNoConnection(string Categoria)
        //{
        //    var result = await App.Database.GetCategoriesList(Categoria);
        //    ListLocal = new List<Categoria>(result);
        //}

        //public async void GetEngAsync(string Categoria)
        //{
        //    English = true;
        //    Portuguese = false;

        //    ListLocal = new List<Categoria>(await TraslateList(await App.Database.GetCategoriesList(Categoria)));

        //    CategoriasIsBusy = false;
        //}

        //public async Task<IEnumerable<Categoria>> TraslateList(List<Categoria> list)
        //{
        //    var Url = TranslateService.TranslateStringBuilder(list);
        //    var Translate = await TranslateService.GetTranslatePtEnList(Url);

        //    return TranslateService.Replace(list, Translate);
        //}

        //public async void TraslateTextCategoria(string Categoria)
        //{
        //    var Url = TranslateService.TranslateStringBuilder(Categoria);

        //    this.Categoria = await TranslateService.GetTranslatePtEn(Url);
        //}
    }
}
