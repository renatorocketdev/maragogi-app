using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.View.Maragogi;
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
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                AddFromAPIAsync();
            }
            else
            {
                AddFromDataBaseAsync();
            }
        }

        #endregion Constructors

        #region Properties

        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        #endregion Properties

        #region Methods

        public async void AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            App.Database.DeleteCategoriasMaragogi();

            ListLocal = await new Service<CategoriaMaragogi>().Get("APICategoriasMaragogi");

            App.Database.SaveCategoriasMaragogi(ListLocal);

            CategoriasIsBusy = false;
        }

        public async void AddFromDataBaseAsync()
        {
            ListLocal = await App.Database.GetCategoriesMaragogiList();
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion Methods
    }
}