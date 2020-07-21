using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
using AppTesteBinding.View.Details;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    class EmpresasDetailsViewModel : INotifyPropertyChanged
    {
        #region PropertyChange
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        Empresa _EmpresaLocal;
        public Empresa EmpresaLocal
        {
            get { return _EmpresaLocal; } 
            set { _EmpresaLocal = value; OnPropertyChanged(nameof(EmpresaLocal)); }
        }

        string _Endereco;
        public string Endereco
        {
            get { return _Endereco; } 
            set { _Endereco = value; OnPropertyChanged(nameof(Endereco)); }
        }

        #region ImagensIsBusy

        ObservableCollection<FotosEstabelecimentos> _Fotos;
        public ObservableCollection<FotosEstabelecimentos> Fotos
        {
            get { return _Fotos; }
            set { _Fotos = value; OnPropertyChanged(nameof(Fotos)); }
        }

        bool _FotoIsBusy;
        public bool FotoIsBusy
        {
            get { return _FotoIsBusy; }
            set { _FotoIsBusy = value; OnPropertyChanged(nameof(FotoIsBusy)); }

        }

        #endregion

        #region ContatosIsBusy

        bool _Telefone2Empresa;
        bool _Facebook;
        bool _Instagram;
        bool _Site;

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


        #endregion

        #region ServiceIsBusy
        bool _Serv2IsVisible;
        bool _Serv3IsVisible;
        bool _Serv4IsVisible;

        public bool Serv2IsVisible
        { 
            get { return _Serv2IsVisible; } set { _Serv2IsVisible = value; OnPropertyChanged(nameof(Serv2IsVisible)); }
        }
        public bool Serv3IsVisible
        {
            get { return _Serv3IsVisible; } set { _Serv3IsVisible = value; OnPropertyChanged(nameof(Serv3IsVisible)); }
        }
        public bool Serv4IsVisible
        {
            get { return _Serv4IsVisible; } set { _Serv4IsVisible = value; OnPropertyChanged(nameof(Serv4IsVisible)); }
        }
        #endregion

        #region CaracteristicasIsBusy
        bool _Carac2IsVisible;
        bool _Carac3IsVisible;
        bool _Carac4IsVisible;

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
        #endregion

        public ICommand CmdLigar { get; }
        public ICommand CmdCompartilhar { get; }
        public ICommand CmdTirarFoto { get; }
        public ICommand CmdGaleria { get; }

        public EmpresasDetailsViewModel()
        {
            
        }

        public EmpresasDetailsViewModel(Empresa empresa)
        {
            EmpresaLocal = empresa;

            CmdLigar = new Command(async (x) => await LigarNumero());
            CmdTirarFoto = new Command(async () => await TirarFoto());
            CmdCompartilhar = new Command<string>(async (x) => await Compartilhar());
            CmdGaleria = new Command<string>(async (x) => await AbrirGaleria());

            ImagemFundo(empresa.NomeEmpresa);

            GetLocalAddress(empresa.NomeEmpresa);

            ValidateStacks();
        }

        private async Task AbrirGaleria()
        {
            if (Settings.Logado || Settings.Facebook)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PageGaleriaComentarioFoto(EmpresaLocal.NomeEmpresa));
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
                await Application.Current.MainPage.Navigation.PushAsync(new PageComentarioFoto(EmpresaLocal.NomeEmpresa));
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
                Text = $"Olha, estou recomendando essa empresa para você, {EmpresaLocal.NomeEmpresa}. " +
                $"\n É uma empresa no ramo {EmpresaLocal.MainCategoria}, especificamente de {EmpresaLocal.SubCategoria}." +
                $"\n O seu principal serviço é {EmpresaLocal.Serv1}." +
                $"\n Localizada em {EmpresaLocal.Endereco}." +
                $"\n Veja mais no nosso APP, venha fazer parte dessa família e descubra muitas empresas de Maragogi!" +
                $"\n https://play.google.com/store/apps/details?id=com.TrTecnologias.Maragogi"
            }); ;
        }

        private async Task LigarNumero()
        {
            await Launcher.OpenAsync(new Uri("tel:" + EmpresaLocal.Telefone1Empresa.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")));
        }

        private void ValidateStacks()
        {
            Serv2IsVisible = IsNull(EmpresaLocal.Serv2);
            Serv3IsVisible = IsNull(EmpresaLocal.Serv3);
            Serv4IsVisible = IsNull(EmpresaLocal.Serv4);

            Carac2IsVisible = IsNull(EmpresaLocal.Carac2);
            Carac3IsVisible = IsNull(EmpresaLocal.Carac3);
            Carac4IsVisible = IsNull(EmpresaLocal.Carac4);

            Telefone2Empresa = IsNull(EmpresaLocal.Telefone2Empresa);
            Facebook = IsNull(EmpresaLocal.Facebook);
            Instagram = IsNull(EmpresaLocal.Instagram);
            Site = IsNull(EmpresaLocal.Site);
        }

        public async void ImagemFundo(string NomeEmpresa)
        {
            EmpresaService EmpresaService = new EmpresaService();

            FotoIsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var result = await EmpresaService.GetFundoEmpresaPath(NomeEmpresa);

                if (result != null)
                {
                    Fotos = new ObservableCollection<FotosEstabelecimentos>(result);
                }
                else
                {
                    Fotos = new ObservableCollection<FotosEstabelecimentos>();
                }
            }
            else
            {
                Fotos = new ObservableCollection<FotosEstabelecimentos>
                {
                    new FotosEstabelecimentos { Foto = "fundooffline.png" }
                };
            }

            FotoIsBusy = false;
        }

        public bool IsNull(string propriedade)
        {
            if (string.IsNullOrEmpty(propriedade))
                return false;

            return true;
        }

        public void GetLocalAddress(string Empresa)
        {
            Endereco = string.Format("https://www.google.com.br/maps/search/{0}", Empresa.Replace(" ", "%20"));
        }
    }
}
