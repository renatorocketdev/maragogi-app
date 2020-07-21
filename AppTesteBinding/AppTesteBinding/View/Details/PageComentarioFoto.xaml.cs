using AppTesteBinding.Models;
using AppTesteBinding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Details
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageComentarioFoto : ContentPage
    {
        public PageComentarioFoto(string nome)
        {
            InitializeComponent();

            BindingContext = new FotoComentarioViewModel(nome);

            var trg = new TapGestureRecognizer();
            trg.Tapped += (s, e) => OnLabelClicked();
            lblVoltar.GestureRecognizers.Add(trg);
        }

        private void OnLabelClicked()
        {
            Navigation.PopAsync();
        }
    }
}