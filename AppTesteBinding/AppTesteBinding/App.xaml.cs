using AppTesteBinding.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Threading.Tasks;
using AppTesteBinding.Data;
using System.IO;
using AppTesteBinding.Utils;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTesteBinding
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseAPI.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            if (Settings.Logado || Settings.Facebook)
                MainPage = new NavigationPage(new MenuPage());
            else
                MainPage = new NavigationPage(new PageLogin());
        }

        public static Action HideLoginView
        {
            get
            {
                return new Action(() => App.Current.MainPage.Navigation.PopModalAsync());
            }
        }

        public async static Task NavigateToProfile()
        {
            await App.Current.MainPage.Navigation.PushAsync(new MenuPage());
        }

        public async static Task ReturnToPageRoot()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        protected override void OnStart()
        {
            AppCenter.Start("a5a858b9-60c6-4e51-bc06-fb1a391ede86;", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
