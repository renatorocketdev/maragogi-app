using AppTesteBinding.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Maragogi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHistoriaMaragogiDetails : ContentPage
    {
        public PageHistoriaMaragogiDetails()
        {
            InitializeComponent();

            BindingContext = new HistoriaMaragogiDetailsViewModel();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}