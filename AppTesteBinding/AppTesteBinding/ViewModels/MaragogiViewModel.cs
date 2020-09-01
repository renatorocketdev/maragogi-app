using AppTesteBinding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    internal class MaragogiViewModel
    {
        #region Fields

        public List<CategoriaMaragogi> ListLocal { get; set; }

        #endregion Fields

        #region Constructors

        public MaragogiViewModel()
        {
            ListLocal = new List<CategoriaMaragogi>()
            {
                new CategoriaMaragogi
                {
                    Foto = "menumaragogi",
                    Nome = "História"
                },
                new CategoriaMaragogi
                {
                    Foto = "menumaragogi",
                    Nome = "Pontos Turísticos"
                },
                new CategoriaMaragogi
                {
                    Foto = "menumaragogi",
                    Nome = "Praias"
                }
            };
        }

        #endregion Constructors

        #region Properties

        public ICommand CmdPopAsync { get { return new Command(async () => await PopAsync()); } }

        #endregion Properties

        #region Methods

        private async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion Methods

    }
}