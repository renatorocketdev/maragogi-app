using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
using AppTesteBinding.View.Details;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    public class HistoriaMaragogiDetailsViewModel : Manager<HistoriaMaragogi>
    {
        #region Fields

        private HistoriaMaragogi _HistoriaMaragogi;

        #endregion Fields

        #region Properties

        public ICommand CmdCompartilhar { get { return new Command(async () => await Compartilhar()); } }

        public ICommand CmdGaleria { get { return new Command(async () => await AbrirGaleria()); } }

        public ICommand CmdTirarFoto { get { return new Command(async () => await TirarFoto()); } }

        public HistoriaMaragogi HistoriaMaragogi
        {
            get { return _HistoriaMaragogi; }
            set { _HistoriaMaragogi = value; OnPropertyChanged(nameof(HistoriaMaragogi)); }
        }

        #endregion Properties

        #region Constructors

        public HistoriaMaragogiDetailsViewModel()
        {
            _ = AddFromAPI();

            _ = AddImagesFromAPIAsync();
        }

        #endregion Constructors

        #region Methods

        private async Task AbrirGaleria()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageGaleriaComentarioFoto(HistoriaMaragogi.Titulo));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }

        private async Task AddFromAPI()
        {
            CategoriasIsBusy = true;
            
            HistoriaMaragogi = await new HistoriaMaragogiService().GetHistoriaMaragogi();

            CategoriasIsBusy = false;
        }

        private async Task AddImagesFromAPIAsync()
        {
            FotoIsBusy = true;

            Fotos = await new HistoriaMaragogiService().GetFotoFundo();

            if (Fotos == null)
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };
            }

            FotoIsBusy = false;
        }

        private async Task Compartilhar()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "App Maragogi",
                Text = "Sobre Maragogi" +
                $"\n {HistoriaMaragogi.Descricao}" +
                "Saiba mais em nosso App" +
                $"\n https://play.google.com/store/apps/details?id=com.TrTecnologias.Maragogi"
            });
        }

        private async Task TirarFoto()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageComentarioFoto(HistoriaMaragogi.Titulo));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }

        #endregion Methods
    }
}