using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTesteBinding.Models;
using AppTesteBinding.Utils;
using AppTesteBinding.View.HasAccessInternet.Categories;
using AppTesteBinding.View.HasNoAccessInternet;
using AppTesteBinding.View.MainPages.English;
using AppTesteBinding.View.MenuPrincipal;
using AppTesteBinding.View.SecondPages;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    class MenuPageViewModel
    {
        string _displaymessage = null;

        public ICommand CmdNavigation { get; }
        public ICommand CmdOpenWhats { get { return new Command(async () => await this.OpenWhats()); } }
        public string Displaymessage { get => _displaymessage; set => _displaymessage = value; }

        public MenuPageViewModel()
        {
            Saudacao();

            CmdNavigation = new Command<string> (async (x) => await Navigation(x));
        }

        public void Saudacao()
        {
            if (string.IsNullOrEmpty(Settings.Usuario))
            {
                if(Settings.Ingles)
                    Displaymessage = "Welcome User!";
                else
                    Displaymessage = "Seja bem-vindo usuário!";
            }
            else
            {
                if (Settings.Ingles)
                    Displaymessage = string.Format("Welcome {0}!", Settings.Usuario);
                else
                    Displaymessage = string.Format("Seja bem-vindo {0}!", Settings.Usuario);
            }
        }

        public async Task Navigation(string pagina)
        {
            ColetaDados(pagina);

            if (pagina == "TrTecnologias")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new TrTecnologias());
            }
            else if (pagina == "Config")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new pageConfig());
            }
            else if (pagina == "Compartilhar")
            {
                await Application.Current.MainPage.DisplayAlert("Em Breve", "Atualizações Serão Lançadas!!!", "OK");
            }
            else if (pagina == "Maragogi")
            {
                ColetaDados(pagina);
                await Application.Current.MainPage.Navigation.PushAsync(new View.SecondPages.PageMaragogi());
            }
            else if (pagina == "PageInformacao")
            {
                ColetaDados(pagina);
                await Application.Current.MainPage.Navigation.PushAsync(new PageInformacao());
            }
            else if (pagina == "PageLocalizacao")
            {
                ColetaDados(pagina);
                await Application.Current.MainPage.Navigation.PushAsync(new PageLocalizacao());
            }
            else
            {
                if (!Settings.Ingles)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new MainCategoriasPage(pagina));
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new PageMainCategoriaEnglish(pagina));
                }
            }
        }

        public async Task OpenWhats()
        {
            var message = "Oi, Tudo Bem? Encontrei sua empresa no App Maragogi e gostaria de saber mais informações sobre a sua empresa!";
            var cleanNumber = "5582981245920";

            try
            {
                await Launcher.OpenAsync(new Uri($"https://wa.me/{cleanNumber}?text={message}"));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void ColetaDados(string Pagina)
        {
            Analytics.TrackEvent("Menu Principal", new Dictionary<string, string>
            {
                { "Pagina", Pagina}
            });
        }
    }
}
