﻿using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppTesteBinding.ViewModels
{
    internal class GaleriaComentarioFotoViewModel : INotifyPropertyChanged
    {
        #region Fields

        private List<FotoComentario> _FotoComentario;

        private bool _ListIsBusy;
        private bool _ListIsVisible;

        #endregion Fields

        #region Properties

        public List<FotoComentario> FotoComentario
        {
            get { return _FotoComentario; }
            set { _FotoComentario = value; OnPropertyChanged(nameof(FotoComentario)); }
        }

        public bool ListIsBusy
        {
            get { return _ListIsBusy; }
            set { _ListIsBusy = value; OnPropertyChanged(nameof(ListIsBusy)); }
        }
        public bool ListIsVisible
        {
            get { return _ListIsVisible; }
            set { _ListIsVisible = value; OnPropertyChanged(nameof(ListIsVisible)); }
        }

        #endregion Properties

        #region Constructors

        public GaleriaComentarioFotoViewModel()
        {
        }

        public GaleriaComentarioFotoViewModel(string NomeEmpresa)
        {
            ListIsBusy = true;

            GetImagesAsync(NomeEmpresa).Wait();

            ListIsBusy = false;
        }

        #endregion Constructors

        #region Methods

        public async Task GetImagesAsync(string NomeEmpresa)
        {
            ListIsVisible = false;

            FotoComentario = await new FotoComentarioService().Get(NomeEmpresa);

            if (FotoComentario != null)
            {
                ListIsVisible = false;
            }
            else
            {
                ListIsVisible = true;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion Methods

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events
    }
}