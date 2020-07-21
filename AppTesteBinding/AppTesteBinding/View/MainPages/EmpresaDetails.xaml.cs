using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTesteBinding.ViewModels;
using AppTesteBinding.Models;

namespace AppTesteBinding.View.Empresas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpresaDetails : ContentPage
    {
        EmpresasDetailsViewModel EmpresasDetailsViewModel;
        public EmpresaDetails(Empresa Empresa)
        {
            InitializeComponent();

            BindingContext = new EmpresasDetailsViewModel(Empresa);

            StartSlide(Empresa);
        }

        private void StartSlide(Empresa NomeEmpresa)
        {
            EmpresasDetailsViewModel = new EmpresasDetailsViewModel(NomeEmpresa);

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    MainCarouselView.Position = (MainCarouselView.Position + 1) % EmpresasDetailsViewModel.Fotos.Count;

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private void Comentar_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Em Breve", "Logo vamos atualizar essa função", "Ok");

            //string filtro = string.Format("Empresa={0}", nomeEmpresa);
            //Navigation.PushAsync(new PageTeste(filtro, nomeEmpresa, Utils.Settings.Usuario, categoriaEmpresa));
        }

        private async void ImageButton_ClickedAsync(object sender, EventArgs e) => await Navigation.PopAsync();

    }
}