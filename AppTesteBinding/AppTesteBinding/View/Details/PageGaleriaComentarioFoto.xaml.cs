using AppTesteBinding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.Details
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGaleriaComentarioFoto : ContentPage
    {
        public PageGaleriaComentarioFoto(string NomeEmpresa)
        {
            InitializeComponent();

            BindingContext = new GaleriaComentarioFotoViewModel(NomeEmpresa);

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