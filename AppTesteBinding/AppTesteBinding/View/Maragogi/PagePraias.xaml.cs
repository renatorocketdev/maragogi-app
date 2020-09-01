using AppTesteBinding.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Maragogi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePraias : ContentPage
    {
        public PagePraias()
        {
            InitializeComponent();
        }

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Praias Item = e.Item as Praias;

            Lista.SelectedItem = -0;

            Navigation.PushAsync(new PagePraiaDetails(Item));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}