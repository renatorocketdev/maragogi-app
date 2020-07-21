using AppTesteBinding.Models;
using AppTesteBinding.View.Empresas;
using AppTesteBinding.ViewModels;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.HasAccessInternet.Business
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubCategoriasPage : ContentPage
    {
        SubCategoriasViewModel SubCategoriasViewModel;
        public SubCategoriasPage(string Categoria)
        {
            InitializeComponent();

            BindingContext = new SubCategoriasViewModel(Categoria);

            Analytics.TrackEvent(Categoria);

            StartSlide(Categoria);
        }

        public void StartSlide(string Categoria)
        {
            SubCategoriasViewModel = new SubCategoriasViewModel(Categoria);

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    MainCarouselView.Position = (MainCarouselView.Position + 1) % SubCategoriasViewModel.Fotos.Count;

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private void Voltar_Clicked(object sender, System.EventArgs e) => Navigation.PopAsync();

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Empresa ItemMenu = e.Item as Empresa;

            Lista.SelectedItem = -0;

            Navigation.PushAsync(new EmpresaDetails(ItemMenu));
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