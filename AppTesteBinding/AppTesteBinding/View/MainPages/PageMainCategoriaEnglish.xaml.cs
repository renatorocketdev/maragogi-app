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

namespace AppTesteBinding.View.MainPages.English
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainCategoriaEnglish : ContentPage
    {
        MainCategoriaEnglishViewModel MainCategoriaEnglishViewModel;
        public PageMainCategoriaEnglish(string Categoria)
        {
            InitializeComponent();

            BindingContext = new MainCategoriaEnglishViewModel(Categoria);

            Analytics.TrackEvent(Categoria);

            StartSlide(Categoria);
        }

        public void StartSlide(string Categoria)
        {
            MainCategoriaEnglishViewModel = new MainCategoriaEnglishViewModel(Categoria);

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    MainCarouselView.Position = (MainCarouselView.Position + 1) % MainCategoriaEnglishViewModel.Fotos.Count;

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private void Voltar_Clicked(object sender, System.EventArgs e) => Navigation.PopAsync();

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Cateogoria = e.Item as CategoriaEnglish;

            Lista.SelectedItem = -1;

            if (Lista != null)
            {
                await Navigation.PushAsync(new SubCategoriasPage(Cateogoria.SubCategoria));
            }
            else
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
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