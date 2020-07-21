using AppTesteBinding.Utils;
using AppTesteBinding.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (!Settings.Ingles)
            {
                Beleza.Text = "BELEZA";
                Comer.Text = "COMER";
                Comprar.Text = "COMPRAR";
                Diversao.Text = "DIVERSÃO";
                Dormir.Text = "DORMIR";
                Eventos.Text = "EVENTOS";
                Locomover.Text = "LOCOMOVER";
                Passeios.Text = "PASSEIOS";
                Saude.Text = "SAÚDE";
                Servicos.Text = "SERVIÇOS";
            }
            else
            {
                Beleza.Text = "BEAUTY";
                Comer.Text = "FOOD";
                Comprar.Text = "SHOPPING";
                Diversao.Text = "FUN";
                Dormir.Text = "REST";
                Eventos.Text = "EVENTS";
                Locomover.Text = "TRANSPORT";
                Passeios.Text = "TOUR";
                Saude.Text = "HEALTH";
                Servicos.Text = "SERVICES";
            }
        }

        private async void ImageButton_ClickedAsync(object sender, EventArgs e) => await ShareText();

        public async Task ShareText()
        {
            string Link = "https://play.google.com/store/apps/details?id=com.TrTecnologias.Maragogi";

            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "App Maragogi",
                Text = "Baixe o App Maragogi, Disponivel para Android.",
                Uri = Link
            });
        }
    }
}