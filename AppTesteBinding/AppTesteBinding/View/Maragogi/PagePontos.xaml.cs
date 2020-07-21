using AppTesteBinding.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Maragogi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePontos : ContentPage
    {
        public PagePontos()
        {
            InitializeComponent();
        }

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Item = e.Item as CategoriaMaragogi;

            Navigation.PushAsync(new PagePontoDetails(Item));

            Lista.SelectedItem = -1;
        }
    }
}