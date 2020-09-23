using AppTesteBinding.Data;
using AppTesteBinding.Models;
using AppTesteBinding.Service.Modulo;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    internal class MaragogiViewModel : Manager<CategoriaMaragogi>
    {
        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        public MaragogiViewModel()
        {
            _ = AddFromAPIAsync();
        }

        private async Task AddFromAPIAsync()
        {
            CategoriasIsBusy = true;

            var list = await new Service<CategoriaMaragogi>().Get("APICategoriasMaragogi");

            ObservableCollectionLocal = new ObservableCollection<CategoriaMaragogi>()
            {
                new CategoriaMaragogi
                {
                    Nome = "História"
                },
                new CategoriaMaragogi
                {
                    Nome = "Praias"
                },
                new CategoriaMaragogi
                {
                    Nome = "Pontos Turísticos"
                }
            };

            list.ForEach(x => ObservableCollectionLocal.Add(x));

            CategoriasIsBusy = false;
        }

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}