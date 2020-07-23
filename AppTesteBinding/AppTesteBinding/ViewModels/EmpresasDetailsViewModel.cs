using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
using AppTesteBinding.View.Details;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    internal class EmpresasDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Empresa _EmpresaLocal;

        public Empresa EmpresaLocal
        {
            get { return _EmpresaLocal; }
            set { _EmpresaLocal = value; OnPropertyChanged(nameof(EmpresaLocal)); }
        }

        private string _Endereco;

        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; OnPropertyChanged(nameof(Endereco)); }
        }

        #region ImagensIsBusy

        private ObservableCollection<FotosEstabelecimentos> _Fotos;

        public ObservableCollection<FotosEstabelecimentos> Fotos
        {
            get { return _Fotos; }
            set { _Fotos = value; OnPropertyChanged(nameof(Fotos)); }
        }

        private bool _FotoIsBusy;

        public bool FotoIsBusy
        {
            get { return _FotoIsBusy; }
            set { _FotoIsBusy = value; OnPropertyChanged(nameof(FotoIsBusy)); }
        }

        #endregion ImagensIsBusy

        #region ContatosIsBusy

        private bool _Telefone2Empresa;
        private bool _Facebook;
        private bool _Instagram;
        private bool _Site;
        private bool _HasVideo;

        public bool Telefone2Empresa
        {
            get { return _Telefone2Empresa; }
            set { _Telefone2Empresa = value; OnPropertyChanged(nameof(Telefone2Empresa)); }
        }

        public bool Facebook
        {
            get { return _Facebook; }
            set { _Facebook = value; OnPropertyChanged(nameof(Facebook)); }
        }

        public bool Instagram
        {
            get { return _Instagram; }
            set { _Instagram = value; OnPropertyChanged(nameof(Instagram)); }
        }

        public bool Site
        {
            get { return _Site; }
            set { _Site = value; OnPropertyChanged(nameof(Site)); }
        }
        
        public bool HasVideo
        {
            get { return _HasVideo; }
            set { _HasVideo = value; OnPropertyChanged(nameof(HasVideo)); }
        }

        #endregion ContatosIsBusy

        #region ServiceIsBusy

        private bool _Serv2IsVisible;
        private bool _Serv3IsVisible;
        private bool _Serv4IsVisible;

        public bool Serv2IsVisible
        {
            get { return _Serv2IsVisible; }
            set { _Serv2IsVisible = value; OnPropertyChanged(nameof(Serv2IsVisible)); }
        }

        public bool Serv3IsVisible
        {
            get { return _Serv3IsVisible; }
            set { _Serv3IsVisible = value; OnPropertyChanged(nameof(Serv3IsVisible)); }
        }

        public bool Serv4IsVisible
        {
            get { return _Serv4IsVisible; }
            set { _Serv4IsVisible = value; OnPropertyChanged(nameof(Serv4IsVisible)); }
        }

        #endregion ServiceIsBusy

        #region CaracteristicasIsBusy

        private bool _Carac2IsVisible;
        private bool _Carac3IsVisible;
        private bool _Carac4IsVisible;

        public bool Carac2IsVisible
        {
            get { return _Carac2IsVisible; }
            set { _Carac2IsVisible = value; OnPropertyChanged(nameof(Carac2IsVisible)); }
        }

        public bool Carac3IsVisible
        {
            get { return _Carac3IsVisible; }
            set { _Carac3IsVisible = value; OnPropertyChanged(nameof(Carac3IsVisible)); }
        }

        public bool Carac4IsVisible
        {
            get { return _Carac4IsVisible; }
            set { _Carac4IsVisible = value; OnPropertyChanged(nameof(Carac4IsVisible)); }
        }

        #endregion CaracteristicasIsBusy

        public ICommand CmdLigar { get; }
        public ICommand CmdCompartilhar { get; }
        public ICommand CmdTirarFoto { get; }
        public ICommand CmdGaleria { get; }

        public ICommand CmdAvaliar { get { return new Command(async (x) => await AvaliarEmpresa()); } }

        private async Task AvaliarEmpresa()
        {
            var result = await Application.Current.MainPage
                .DisplayPromptAsync("Deseja Curtir " + EmpresaLocal.NomeEmpresa + "?", "De uma nota de 0 a 10",
                accept: "Curtir", maxLength: 2, keyboard: Keyboard.Numeric);

            if (Convert.ToDouble(result) > 10)
            {
                await Application.Current.MainPage.DisplayAlert("Erro na avaliação", "Você digitou uma nota invalida. \nPor favor digite novamente uma nova valida!", "Ok");
            }
            else
            {
                var avaliacao = new AvaliacaoEmpresa
                {
                    id = EmpresaLocal.IdEmpresa,
                    Nota = Convert.ToDouble(result)
                };

                await new AvaliacaoService().Save(avaliacao);
            }
        }

        public EmpresasDetailsViewModel()
        {
        }

        public EmpresasDetailsViewModel(Empresa empresa)
        {
            this.EmpresaLocal = empresa;

            this.CmdLigar = new Command(async (x) => await this.LigarNumero());
            this.CmdTirarFoto = new Command(async () => await this.TirarFoto());
            this.CmdCompartilhar = new Command<string>(async (x) => await this.Compartilhar());
            this.CmdGaleria = new Command<string>(async (x) => await this.AbrirGaleria());

            this.ImagemFundo(empresa.NomeEmpresa, empresa.SubCategoria);

            this.GetLocalAddress(empresa.NomeEmpresa);

            this.ValidateStacks();
        }

        private async Task AbrirGaleria()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageGaleriaComentarioFoto(this.EmpresaLocal.NomeEmpresa));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }

        private async Task TirarFoto()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageComentarioFoto(this.EmpresaLocal.NomeEmpresa));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Faça login ou cadastre-se no App para poder acessar essa função", "Ok");
            }
        }

        private async Task Compartilhar()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "App Maragogi",
                Text = $"Olha, estou recomendando essa empresa para você, {this.EmpresaLocal.NomeEmpresa}. " +
                $"\n É uma empresa no ramo {this.EmpresaLocal.MainCategoria}, especificamente de {this.EmpresaLocal.SubCategoria}." +
                $"\n O seu principal serviço é {this.EmpresaLocal.Serv1}." +
                $"\n Localizada em {this.EmpresaLocal.Endereco}." +
                $"\n Veja mais no nosso APP, venha fazer parte dessa família e descubra muitas empresas de Maragogi!" +
                $"\n https://play.google.com/store/apps/details?id=com.TrTecnologias.Maragogi",
            }); ;
        }

        private async Task LigarNumero()
        {
            await Launcher.OpenAsync(new Uri("tel:" + this.EmpresaLocal.Telefone1Empresa.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")));
        }

        private void ValidateStacks()
        {
            this.Serv2IsVisible = !string.IsNullOrEmpty(this.EmpresaLocal.Serv2);
            this.Serv3IsVisible = !string.IsNullOrEmpty(this.EmpresaLocal.Serv3);
            this.Serv4IsVisible = !string.IsNullOrEmpty(this.EmpresaLocal.Serv4);

            this.Carac2IsVisible = !string.IsNullOrEmpty(this.EmpresaLocal.Carac2);
            this.Carac3IsVisible = !string.IsNullOrEmpty(this.EmpresaLocal.Carac3);
            this.Carac4IsVisible = !string.IsNullOrEmpty(this.EmpresaLocal.Carac4);

            this.Telefone2Empresa = !string.IsNullOrEmpty(this.EmpresaLocal.Telefone2Empresa);
            this.Facebook = !string.IsNullOrEmpty(this.EmpresaLocal.Facebook);
            this.Instagram = !string.IsNullOrEmpty(this.EmpresaLocal.Instagram);
            this.Site = !string.IsNullOrEmpty(this.EmpresaLocal.Site);

            this.HasVideo = !string.IsNullOrEmpty(this.EmpresaLocal.Video);
        }

        public async void ImagemFundo(string nomeEmpresa, string subCategoria)
        {

            this.FotoIsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await new EmpresaService().GetFundoEmpresaPath(nomeEmpresa, subCategoria);

                if (result != null)
                {
                    this.Fotos = new ObservableCollection<FotosEstabelecimentos>(result);
                }
                else
                {
                    this.Fotos = new ObservableCollection<FotosEstabelecimentos>();
                }
            }
            else
            {
                this.Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" },
                };
            }

            this.FotoIsBusy = false;
        }
        public void GetLocalAddress(string empresa)
        {
            this.Endereco = string.Format("https://www.google.com.br/maps/search/{0}", empresa.Replace(" ", "%20"));
        }
    }
}