using AppTesteBinding.Models;
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
        }

        #endregion Constructors

        #region Properties

        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        #endregion Properties

        #region Methods

        public async Task AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            ListLocal = await new Service<CategoriaMaragogi>().Get("APICategoriasMaragogi", "Categoria", "Pontos");

            CategoriasIsBusy = false;
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion Methods
    }
}