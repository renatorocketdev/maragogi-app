using AppTesteBinding.Models;
using AppTesteBinding.Service;
using AppTesteBinding.Utils;
using AppTesteBinding.View.Details;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    public class CategoriasMaragogiDetailsViewModel : Manager<CategoriaMaragogi>
    {
        private string _Foto;

        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; OnPropertyChanged(nameof(Foto)); }
        }

        private CategoriaMaragogi _CategoriaMaragogi;

        public CategoriaMaragogi CategoriaMaragogiLocal
        {
            get { return _CategoriaMaragogi; }
            set { _CategoriaMaragogi = value; OnPropertyChanged(nameof(CategoriaMaragogiLocal)); }
        }

        public ICommand CmdCompartilhar { get; }
        public ICommand CmdGaleria { get; }
        public ICommand CmdTirarFoto { get; }

        public CategoriasMaragogiDetailsViewModel()
        {
        }

        public CategoriasMaragogiDetailsViewModel(CategoriaMaragogi categoriaMaragogi)
        {
            CategoriaMaragogiLocal = categoriaMaragogi;

            CmdTirarFoto = new Command<CategoriaMaragogi>(async (x) => await TirarFoto());
            CmdCompartilhar = new Command<string>(async (x) => await Compartilhar());
            CmdGaleria = new Command<string>(async (x) => await AbrirGaleria());

            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync(CategoriaMaragogiLocal.Nome);
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };
            }

            if (!string.IsNullOrEmpty(CategoriaMaragogiLocal.Localizacao))
            {
                CategoriaMaragogiLocal.Localizacao = string.Format("https://www.google.com.br/maps/search/{0}", CategoriaMaragogiLocal.Nome.Replace(" ", "%20") + "," + CategoriaMaragogiLocal.Localizacao.Replace(" ", "%20"));
            }
            else
            {
                CategoriaMaragogiLocal.Localizacao = string.Format("https://www.google.com.br/maps/search/{0}", CategoriaMaragogiLocal.Nome.Replace(" ", "%20"));
            }
        }

        private async void AddImagesFromAPIAsync(string categoriaMaragogi)
        {
            FotoIsBusy = true;

            var result = await new MyServiceImage().GetImages("APIFotoCategoriasMaragogi", "CategoriasMaragogi", categoriaMaragogi);

            if (result != null)
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>(result);
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>();
            }

            FotoIsBusy = false;
        }

        private async Task Compartilhar()
        {
            await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            {
                Title = "App Maragogi",
                Text = $"Olha, estou recomendando esse lugar para você, {CategoriaMaragogiLocal.Nome}. " +
                $"\n Localizado em {CategoriaMaragogiLocal.Localizacao}." +
                $"\n Veja mais no nosso APP, venha fazer parte dessa família e descubra muitas empresas de Maragogi!" +
                $"\n https://play.google.com/store/apps/details?id=com.TrTecnologias.Maragogi"
            }); ;
        }

        private async Task TirarFoto()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageComentarioFoto(CategoriaMaragogiLocal.Nome));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }

        private async Task AbrirGaleria()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageGaleriaComentarioFoto(CategoriaMaragogiLocal.Nome));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }
    }
}