using AppTesteBinding.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    class MaragogiViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        List<Categoria> _categoriasLocal;
        public List<Categoria> CategoriasLocal
        {
            get { return _categoriasLocal; }
            set { _categoriasLocal = value; OnPropertyChanged(nameof(CategoriasLocal)); }
        }

        public ICommand CmdPopAsync { get; }

        public MaragogiViewModel()
        {
            CmdPopAsync = new Command(async (x) => await PopAsync());
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
