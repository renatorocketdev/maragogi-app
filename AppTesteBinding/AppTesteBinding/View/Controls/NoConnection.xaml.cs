using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.HasNoAccessInternet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConnection : ContentPage
    {
        public NoConnection()
        {
            InitializeComponent();

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