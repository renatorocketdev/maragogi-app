using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using AppTesteBinding.View.Maragogi;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    internal class CategoriasMaragogiViewModel : INotifyPropertyChanged
    {
        #region Fields

        private List<CategoriaMaragogi> _ListLocal;

        private bool _CategoriasIsBusy;

        #endregion Fields

        #region Constructors

        public CategoriasMaragogiViewModel()
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

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public bool CategoriasIsBusy
        {
            get { return _CategoriasIsBusy; }
            set { _CategoriasIsBusy = value; OnPropertyChanged(nameof(CategoriasIsBusy)); }
        }

        public ICommand CmdHistoriaMaragogi { get { return new Command(async () => await HistoriaMaragogiPage()); } }

        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        public ICommand CmdPraias { get { return new Command(async () => await Navigation()); } }

        public List<CategoriaMaragogi> ListLocal
        {
            get { return _ListLocal; }
            set { _ListLocal = value; OnPropertyChanged(nameof(ListLocal)); }
        }

        #endregion Properties

        #region Methods

        public async void AddFromAPIAsync()
        {
            try
            {
                App.Database.DeleteCategoriasMaragogi();

                _CategoriasIsBusy = true;

                ListLocal = await new CategoriasMaragogiService().GetCategoriasMaragogi();

                _CategoriasIsBusy = false;

                App.Database.SaveCategoriasMaragogi(ListLocal);
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public async void AddFromDataBaseAsync()
        {
            ListLocal = await App.Database.GetCategoriesMaragogiList();
        }

        public async Task Navigation()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PagePraias());
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task HistoriaMaragogiPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PageHistoriaMaragogiDetails());
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion Methods
    }
}