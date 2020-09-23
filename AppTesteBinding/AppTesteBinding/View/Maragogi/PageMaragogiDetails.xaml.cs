using AppTesteBinding.Models;
using AppTesteBinding.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Maragogi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMaragogiDetails : ContentPage
    {
        #region Constructors

        public PageMaragogiDetails(CategoriaMaragogi categoriaMaragogi)
        {
            InitializeComponent();

            BindingContext = new CategoriasMaragogiDetailsViewModel(categoriaMaragogi);
        }

        #endregion Constructors

        #region Methods

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        #endregion Methods
    }
}