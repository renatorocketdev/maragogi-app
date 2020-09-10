using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    public class FotoComentarioViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string _Foto;

        private string _FotoIsBusy;

        private MediaFile MediaFile;

        #endregion Fields

        #region Constructors

        public FotoComentarioViewModel()
        {

        }

        public FotoComentarioViewModel(string Nome)
        {
            FotoIsBusy = "Tire uma foto!";

            CmdTirarFoto = new Command<Empresa>(async (x) => await TirarFoto());
            CmdSalvarFoto = new Command<Empresa>(async (x) => await SalvarAsync(Nome));
        }

        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public ICommand CmdSalvarFoto { get; }

        public ICommand CmdTirarFoto { get; }

        public string Comment { get; set; } = null;

        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; OnPropertyChanged(nameof(Foto)); }
        }

        public string FotoIsBusy
        {
            get { return _FotoIsBusy; }
            set { _FotoIsBusy = value; OnPropertyChanged(nameof(FotoIsBusy)); }
        }

        #endregion Properties

        #region Methods

        public async Task<bool> HasPermission()
        {
            var Permission = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();

            if (Permission == PermissionStatus.Denied)
                Permission = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();

            if (Permission == PermissionStatus.Granted)
                return true;

            return false;
        }

        public async Task SalvarAsync(string Nome)
        {
            if (MediaFile != null)
            {
                byte[] vs = null;

                using (var memoryStream = new MemoryStream())
                {
                    MediaFile.GetStream().CopyTo(memoryStream);
                    MediaFile.Dispose();
                    vs = memoryStream.ToArray();
                }

                FotoComentario comentario = new FotoComentario
                {
                    Comentario = Comment,
                    DataComentario = DateTime.Now.ToString(),
                    Empresa = Nome,
                    Foto = vs,
                    Usuario = Settings.Usuario,
                    Email = Settings.Email
                };

                await new Service<FotoComentario>().Post(comentario, "APIFotoComentario");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Impossível Postar Foto", "Primeiro tire uma foto", "OK");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task TirarFoto()
        {
            await CrossMedia.Current.Initialize();

            try
            {
                if (await HasPermission())
                {
                    MediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = Settings.Usuario,
                        Name = $"{Settings.Usuario}.jpg",
                        CompressionQuality = 75,
                        CustomPhotoSize = 50,
                    });

                    if (MediaFile != null)
                        FotoIsBusy = "Carregando Foto....";
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Sem Permissão", "Permita a camera para nosso App poder tirar fotos", "OK");

                    CrossPermissions.Current.OpenAppSettings();
                }

                if (MediaFile == null)
                    return;

                Foto = MediaFile.Path;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }


        }

        #endregion Methods
    }
}