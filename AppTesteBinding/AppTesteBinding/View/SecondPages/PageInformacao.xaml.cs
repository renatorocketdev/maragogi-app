using System;
using AppTesteBinding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.MenuPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageInformacao : ContentPage
	{
		public PageInformacao ()
		{
			InitializeComponent ();

            var trg = new TapGestureRecognizer();
            trg.Tapped += (s, e) => OnLabelClicked();
            lblVoltar.GestureRecognizers.Add(trg);
        }

        private void OnLabelClicked()
        {
            Navigation.PopAsync();
        }
    }
}