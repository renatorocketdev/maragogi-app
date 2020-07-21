using AppTesteBinding.Models;
using AppTesteBinding.View.Maragogi;
using AppTesteBinding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.SecondPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCategoriasMaragogi : ContentPage
    {
        public PageCategoriasMaragogi()
        {
            InitializeComponent();

            BindingContext = new CategoriasMaragogiViewModel();
        }

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CategoriaMaragogi Item = e.Item as CategoriaMaragogi;

            Lista.SelectedItem = -0;

            Navigation.PushAsync(new PageCategoriasMaragogiDetails(Item));
        }
    }
}