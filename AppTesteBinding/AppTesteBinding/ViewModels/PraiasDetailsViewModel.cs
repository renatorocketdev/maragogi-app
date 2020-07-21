using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
using AppTesteBinding.View.Details;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    public class PraiasDetailsViewModel : Manager<Praias>
    {
        string _Foto;
        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; OnPropertyChanged(nameof(Foto)); }
        }

        private Praias _PraiaLocal;
        public Praias PraiaLocal
        {
            get { return _PraiaLocal; }
            set { _PraiaLocal = value; OnPropertyChanged(nameof(PraiaLocal)); }
        }

        string _Comment = null;
        public string Comment { get => _Comment; set => _Comment = value; }

        public ICommand CmdCompartilhar { get; }
        public ICommand CmdGaleria { get; }
        public ICommand CmdTirarFoto { get; }

        public PraiasDetailsViewModel()
        {

        }

        public PraiasDetailsViewModel(Praias praias)
        {
            PraiaLocal = praias;

            CmdTirarFoto = new Command<Praias>(async (x) => await TirarFoto());
            CmdCompartilhar = new Command<string>(async (x) => await Compartilhar());
            CmdGaleria = new Command<string>(async (x) => await AbrirGaleria());

            if (Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                AddImagesFromAPIAsync(PraiaLocal.Nome);
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };
            }

            if (!string.IsNullOrEmpty(PraiaLocal.Localizacao))
            {
                PraiaLocal.Localizacao = string.Format("https://www.google.com.br/maps/search/{0}", PraiaLocal.Nome.Replace(" ", "%20") + "," + PraiaLocal.Localizacao.Replace(" ", "%20"));
            }
            else
            {
                PraiaLocal.Localizacao = string.Format("https://www.google.com.br/maps/search/{0}", PraiaLocal.Nome.Replace(" ", "%20"));
            }
            
        }

        private async void AddImagesFromAPIAsync(string Praia)
        {
            FotoIsBusy = true;

            var result = await new PraiasService().GetFotoFundo(Praia);

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
                Text = $"Olha, estou recomendando essa praia para você, {PraiaLocal.Nome}. " +
                $"\n Localizada em {PraiaLocal.Localizacao}." +
                $"\n Veja mais no nosso APP, venha fazer parte dessa família e descubra muitas empresas de Maragogi!" +
                $"\n https://play.google.com/store/apps/details?id=com.TrTecnologias.Maragogi"
            }); ;
        }

        private async Task TirarFoto()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageComentarioFoto(PraiaLocal.Nome));
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
                await Application.Current.MainPage.Navigation.PushAsync(new PageGaleriaComentarioFoto(PraiaLocal.Nome));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }
    }
}
