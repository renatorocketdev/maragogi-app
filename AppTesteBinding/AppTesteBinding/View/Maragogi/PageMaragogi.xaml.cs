using AppTesteBinding.Models;
using AppTesteBinding.View.Maragogi;
using AppTesteBinding.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.SecondPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMaragogi : ContentPage
    {
        public PageMaragogi()
        {
            InitializeComponent();

            BindingContext = new MaragogiViewModel();
        }

        private async void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Item = e.Item as CategoriaMaragogi;

            Lista.SelectedItem = -0;

            if (Item.Nome.Equals("História"))
            {
                await Navigation.PushAsync(new PageHistoriaMaragogiDetails());
            }
            else if (Item.Nome.Equals("Praias"))
            {
                await Navigation.PushAsync(new PagePraias());
            }
            else if (Item.Nome.Equals("Pontos Turísticos"))
            {
                await Navigation.PushAsync(new PagePontos());
            }
            else
            {
                await Navigation.PushAsync(new PageMaragogiDetails(Item));
            }
        }
    }
}