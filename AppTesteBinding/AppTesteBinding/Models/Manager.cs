using AppTesteBinding.Service;
using AppTesteBinding.Service.Modulo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppTesteBinding.Models
{
    public class Manager<T> : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChanged

        #region Fields

        public List<FotosEstabelecimentos> _FotoCategoria;
        public List<T> _ListLocal;
        public ObservableCollection<FotosEstabelecimentos> _Fotos;
        public TranslateService TranslateService = new TranslateService();

        private bool _CategoriasIsBusy;
        private bool _English;
        private bool _FotoIsBusy;
        private bool _HasNoEnteprises;
        private bool _Portuguese;
        private string _Categoria;

        #endregion Fields

        #region Properties
        public string Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; OnPropertyChanged(nameof(Categoria)); }
        }

        public bool CategoriasIsBusy
        {
            get { return _CategoriasIsBusy; }
            set { _CategoriasIsBusy = value; OnPropertyChanged(nameof(CategoriasIsBusy)); }
        }

        public bool English
        {
            get { return _English; }
            set { _English = value; OnPropertyChanged(nameof(English)); }
        }

        public List<FotosEstabelecimentos> FotoCategoria
        {
            get { return _FotoCategoria; }
            set { _FotoCategoria = value; OnPropertyChanged(nameof(FotoCategoria)); }
        }

        public bool FotoIsBusy
        {
            get { return _FotoIsBusy; }
            set { _FotoIsBusy = value; OnPropertyChanged(nameof(FotoIsBusy)); }
        }

        public ObservableCollection<FotosEstabelecimentos> Fotos
        {
            get { return _Fotos; }
            set { _Fotos = value; OnPropertyChanged(nameof(Fotos)); }
        }

        public bool HasNoEnterprises
        {
            get { return _HasNoEnteprises; }
            set { _HasNoEnteprises = value; OnPropertyChanged(nameof(HasNoEnterprises)); }
        }

        public List<T> ListLocal
        {
            get { return _ListLocal; }
            set { _ListLocal = value; OnPropertyChanged(nameof(ListLocal)); }
        }

        public bool Portuguese
        {
            get { return _Portuguese; }
            set { _Portuguese = value; OnPropertyChanged(nameof(Portuguese)); }
        }

        #endregion Properties
    }
}