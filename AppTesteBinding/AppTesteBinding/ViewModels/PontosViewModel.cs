using AppTesteBinding.Models;
using AppTesteBinding.Service;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.View.Maragogi;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    internal class PontosViewModel : Manager<CategoriaMaragogi>
    {
        #region Constructors

        public PontosViewModel()
        {
            _ = AddFromAPIAsync();
            _ = AddImagesFromAPIAsync();
        }

        #endregion Constructors

        #region Properties

        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        #endregion Properties

        #region Methods

        public async Task AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            ListLocal = await new Service<CategoriaMaragogi>().Get("APICategoriasMaragogi", "Categoria", "Pontos Turísticos");

            CategoriasIsBusy = false;
        }

        private async Task AddImagesFromAPIAsync()
        {
            FotoIsBusy = true;

            Fotos = await new MyServiceImage().GetImages("APIFotoCategoriasMaragogi", "tipo", "FundoPontoTuristico");

            FotoIsBusy = false;
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion Methods
    }
}