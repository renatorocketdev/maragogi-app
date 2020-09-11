using AppTesteBinding.Models;
using AppTesteBinding.View.HasAccessInternet.Business;
using AppTesteBinding.View.HasNoAccessInternet;
using AppTesteBinding.ViewModels;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.HasAccessInternet.Categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCategoriasPage : ContentPage
    {
        MainCategoriasViewModel MainCategoriasViewModel;
        public MainCategoriasPage(string Categoria)
        {
            InitializeComponent();

            BindingContext = new MainCategoriasViewModel(Categoria);

            Analytics.TrackEvent(Categoria);

            StartSlide(Categoria);
        }

        public void StartSlide(string Categoria)
        {
            MainCategoriasViewModel = new MainCategoriasViewModel(Categoria);

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    MainCarouselView.Position = (MainCarouselView.Position + 1) % MainCategoriasViewModel.Fotos.Count;

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private void Voltar_Clicked(object sender, System.EventArgs e) => Navigation.PopAsync();

        private async void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Cateogoria = e.Item as Categoria;

            listViewPortuguese.SelectedItem = -1;

            if (Cateogoria.SubCategoria.Contains("inicial"))
            {
                await Navigation.PopAsync();
            }
            else
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await Navigation.PushAsync(new SubCategoriasPage(Cateogoria.SubCategoria));
                }
                else
                {
                    await Navigation.PushAsync(new NoConnection());
                }
            }
        }

        public void ColetaDados(string Empresa, string Categoria)
        {
            Analytics.TrackEvent("Empresas", new Dictionary<string, string>
            {
                {"Nome", Empresa },
                {"Categoria", Categoria}
            });
        }
    }
}