using AppTesteBinding.Models;
using AppTesteBinding.Service;
using AppTesteBinding.Service.Modulo;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    internal class MaragogiViewModel : Manager<CategoriaMaragogi>
    {
        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        public MaragogiViewModel()
        {
            _ = AddFromAPIAsync();
            _ = AddImagesFromAPIAsync();
        }

        private async Task AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            var list = await new Service<CategoriaMaragogi>().Get("APICategoriasMaragogi");

            ObservableCollectionLocal = new ObservableCollection<CategoriaMaragogi>()
            {
                new CategoriaMaragogi
                {
                    Nome = "História",
                    Icone = new WebClient().DownloadData(new Uri("https://www.ocaribedemaragogi.com.br/Uploads/historia.png"))
                },
                new CategoriaMaragogi
                {
                    Nome = "Praias",
                    Icone = new WebClient().DownloadData(new Uri("https://www.ocaribedemaragogi.com.br/Uploads/praias.png"))
                },
                new CategoriaMaragogi
                {
                    Nome = "Pontos Turísticos",
                    Icone = new WebClient().DownloadData(new Uri("https://www.ocaribedemaragogi.com.br/Uploads/ponto.png"))
                }
            };

            list.ForEach(x => ObservableCollectionLocal.Add(x));

            CategoriasIsBusy = false;
        }

        private async Task AddImagesFromAPIAsync()
        {
            FotoIsBusy = true;

            Fotos = await new MyServiceImage().GetImages("APIFotoCategoriasMaragogi", "tipo", "FundoMaragogi");

            FotoIsBusy = false;
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}