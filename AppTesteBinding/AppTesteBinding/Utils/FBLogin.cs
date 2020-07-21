using AppTesteBinding.View;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTesteBinding.Utils
{
    public class FBLogin : Application
    {
        public static Action HideLoginView
        {
            get
            {
                return new Action(() => App.Current.MainPage.Navigation.PopModalAsync());
            }
        }

        public async static Task NavigateToMenu()
        {
            await FBLogin.Current.MainPage.Navigation.PopAsync();
        }

        public FBLogin()
        {
            MainPage = new NavigationPage(new MenuPage());
        }
    }
}
