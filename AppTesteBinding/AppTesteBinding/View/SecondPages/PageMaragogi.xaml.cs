using AppTesteBinding.Models;
using AppTesteBinding.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.MenuPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMaragogi : ContentPage
    {
        public PageMaragogi()
        {
            InitializeComponent();
        }

        private void ImgbtnVoltar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    listView.ItemsSource = await App.Database.GetCategoriasAsync();
        //}
    }
}