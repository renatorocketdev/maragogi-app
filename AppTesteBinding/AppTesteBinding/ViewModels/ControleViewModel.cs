using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AppTesteBinding.Utils;
using AppTesteBinding.View;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace AppTesteBinding.ViewModels
{
    class ControleViewModel
    {
        #region Variaveis
        string _logar = null;
        string _registrar = null;
        string _voltar = null;
        string _confirmar = null;
        string _pularCadastro = null;
        string _usuario = null;
        string _senha = null;
        string _email = null;
        string _telefone = null;
        bool _lembrar = false;
        #endregion
        public ICommand CmdPularCadastro { get; }
        public ICommand CmdEfetuarLogin { get; }
        public ICommand CmdEfetuarCadastro { get; }
        public ICommand CmdNavigation { get; }

        public string Logar { get => _logar; set => _logar = value; }
        public string Registrar { get => _registrar; set => _registrar = value; }
        public bool Lembrar { get => _lembrar; set => _lembrar = value; }
        public string Pularcadastro { get => _pularCadastro; set => _pularCadastro = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public string Telefone { get => _telefone; set => _telefone = value; }
        public string Email { get => _email; set => _email = value; }
        public string Voltar { get => _voltar; set => _voltar = value; }
        public string Confirmar { get => _confirmar; set => _confirmar = value; }

        public ControleViewModel()
        {
            Traducao();

            Lembrar = LembrarConta();

            CmdPularCadastro = new Command(async () => await PularCadastro());
            CmdEfetuarLogin = new Command(async () => await EfetuarLogin());
            CmdEfetuarCadastro = new Command(async () => await EfetuarCadastro());
            CmdNavigation = new Command<string>(async (x) => await Navegacao(x));
        }

        public async Task Navegacao(string pagina)
        {
            if (pagina == "Registrar")
            {
                Analytics.TrackEvent("Pag Registrar");

                await Application.Current.MainPage.Navigation.PushAsync(new PageRegistrar());
            }
            else if(pagina == "Facebook")
            {
                Analytics.TrackEvent("Pag Facebook");

                await Application.Current.MainPage.Navigation.PushAsync(new Login());
            }
            else if(pagina == "RegistrarLogin")
                await Application.Current.MainPage.Navigation.PopAsync();
                
        }

        public void Traducao()
        {
            if (Settings.Ingles)
            {
                Logar = "Log In";
                Registrar = "Would You Like To Register?";
                Pularcadastro = "Skip Registration";

                Voltar = "Previous";
                Confirmar = "Finish";
            }
            else
            {
                Logar = "Logar";
                Registrar = "Criar Conta";
                Pularcadastro = "Pular Cadastro";

                Voltar = "Voltar";
                Confirmar = "Concluir";
            }
        }

        public bool LembrarConta()
        {
            if (Settings.Lembrar)
            {
                Usuario = Settings.Usuario;
                Senha = Settings.Senha;
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task PularCadastro()
        {
            Analytics.TrackEvent("Pular Cadastro");

            await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
        }

        public async Task EfetuarLogin()
        {
            if (string.IsNullOrEmpty(Senha) || string.IsNullOrEmpty(Usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Preencha os campos!!", "Ok");
            }
            else if (Settings.Usuario != Usuario && Settings.Senha != Senha)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Senha ou usuário incorretos!!", "Ok");
            }
            else if (Settings.Usuario == Usuario && Settings.Senha == Senha)
            {
                Settings.Logado = true;
                Settings.Facebook = false;

                await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
            }
                
        }

        public async Task EfetuarCadastro()
        {
            Analytics.TrackEvent("Pag Registrar");

            if (!string.IsNullOrEmpty(Usuario) || !string.IsNullOrEmpty(Telefone) || !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Senha))
            {
                Settings.Usuario = Usuario;
                Settings.Telefone = Telefone;
                Settings.Email = Email;
                Settings.Senha = Senha;

                Settings.Logado = true;
                Settings.Facebook = false;

                Analytics.TrackEvent("Registro", new Dictionary<string, string>
                {
                    { "Logado", "Normal"}
                });

                await Application.Current.MainPage.Navigation.PushAsync(new MenuPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Campos Faltam ser Preenchidos", "Ok");
            }
        }
    }
}
