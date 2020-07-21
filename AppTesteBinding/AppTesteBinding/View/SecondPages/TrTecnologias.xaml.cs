using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.MenuPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrTecnologias : ContentPage
    { 

        public TrTecnologias ()
		{
			InitializeComponent ();
            
        }

        private void ImgbtnVoltar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}