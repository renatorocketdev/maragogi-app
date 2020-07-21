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

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CategoriaMaragogi Item = e.Item as CategoriaMaragogi;

            Lista.SelectedItem = -0;

            if (Item.Nome.Equals("Praias"))
            {
                Navigation.PushAsync(new PagePraias());
            }
            else if(Item.Nome.Equals("Pontos Turísticos"))
            {
                Navigation.PushAsync(new PagePontos());
            }
            else
            {
                Navigation.PushAsync(new PageHistoriaMaragogiDetails());
            }

           
        }
    }
}