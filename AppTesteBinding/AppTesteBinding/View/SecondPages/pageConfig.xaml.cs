using AppTesteBinding.Utils;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.MenuPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class pageConfig : ContentPage
	{
		public pageConfig ()
		{
			InitializeComponent ();

            if (!Settings.Ingles)
            {
                btnIdioma.Text = "Ativar Modo Inglês";
                TituloTableLanguage.Title = "Idioma / Conta Vinculada";
                TituloTableDados.Title = "Dados";
                TituloTableInformacao.Title = "Informações";
                btnEditarDados.Text = "Editar Dados";
                lblEditarDados.Text = "Editar Dados";
                entryTelefone.Placeholder = "Telefone";
                lblPrincipalNome.Text = "Nome:";
                lblPrincipalTelefone.Text = "Telefone:";
                btnConfirmarEdicao.Text = "Confirmar";
                btnCancelarEdicao.Text = "Cancelar";
                lblInformacao.Text = "As Imagens e Icones que foram utilizadas no aplicativo são da plataforma https://br.freepik.com";

                if (Settings.Logado)
                {
                    btnConta.Text = "Nenhuma Conta Vinculada";
                }
                else if (!Settings.Logado)
                {
                    btnConta.Text = "Conta Não Registrada";
                }
                else if (Settings.Facebook)
                {
                    btnConta.Text = "Desvincular Conta";
                }
            }
            else
            {
                btnIdioma.Text = "Desable English Mode";
                TituloTableLanguage.Title = "Language / Linked Account";
                TituloTableDados.Title = "Data";
                TituloTableInformacao.Title = "Information";
                btnEditarDados.Text = "Edit Data";
                lblEditarDados.Text = "Edit Data";
                entryTelefone.Placeholder = "Phone";
                lblPrincipalNome.Text = "Name:";
                lblPrincipalTelefone.Text = "Phone:";
                btnConfirmarEdicao.Text = "Confirm";
                btnCancelarEdicao.Text = "Cancel";
                lblInformacao.Text = "The images and icons that were used in the application are from the platform https://br.freepik.com";

                if (Settings.Logado)
                {
                    btnConta.Text = "No Linked Account";
                }
                else if (!Settings.Logado)
                {
                    btnConta.Text = "You Haven't Register Yet";
                }
                else if (Settings.Facebook)
                {
                    btnConta.Text = "Unlink Account";
                }
            }
                

            lblNome.Text = Settings.Usuario;
            lblEmail.Text = Settings.Email;
            lblTelefone.Text = Settings.Telefone;

            tabDivisor.IsVisible = false;
            lblEditarDados.IsVisible = false;
            StackEditarDados.IsVisible = false;

            var trg = new TapGestureRecognizer();
            trg.Tapped += (s, e) => OnLabelClicked();
            lblVoltar.GestureRecognizers.Add(trg);
        }

        private void OnLabelClicked()
        {
            Navigation.PopAsync();
        }

        private void BtnIdioma_Clicked(object sender, EventArgs e)
        {
            if (!Settings.Ingles)
            {
                Settings.Ingles = true;
                Navigation.PopAsync();
            }
            else if (Settings.Ingles)
            {
                Settings.Ingles = false;
                Navigation.PopAsync();
            }
            
        }

        private void BtnConta_Clicked(object sender, EventArgs e)
        {
            if (!Settings.Logado)
            {
                Settings.Email = "";
                Settings.Lembrar = false;
                Settings.Senha = "";
                Settings.Telefone = "";
                Settings.Usuario = "";
                Settings.Logado = false;
                Settings.Facebook = false;

                App.Current.MainPage = new NavigationPage(new PageLogin());
                Navigation.PopToRootAsync();
            }
        }

        private void BtnEditarDados_Clicked(object sender, EventArgs e)
        {
            if (tabDivisor.IsVisible == true)
            {
                if (Settings.Logado || Settings.Facebook)
                {
                    tabDivisor.IsVisible = false;
                    lblEditarDados.IsVisible = false;
                    StackEditarDados.IsVisible = false;
                }
            }
            else
            {
                if (Settings.Logado || Settings.Facebook)
                {
                    tabDivisor.IsVisible = true;
                    lblEditarDados.IsVisible = true;
                    StackEditarDados.IsVisible = true;
                }
            }
        }

        private void BtnConfirmarEdicao_Clicked(object sender, EventArgs e)
        {
            var teste = entryEmail.Text;
            if (entryEmail.Text == "" || entryEmail.Text == null)
            {
                DisplayAlert("Erro", "Email Vazio", "OK");
            }
            else
            {
                Settings.Email = entryEmail.Text.Trim();
            }

            if (entryTelefone.Text == "")
            {
                DisplayAlert("Erro", "Telefone Vazio", "OK");
            }
            else
            {
                Settings.Telefone = entryTelefone.Text;
            }

            tabDivisor.IsVisible = false;
            lblEditarDados.IsVisible = false;
            StackEditarDados.IsVisible = false;

            Navigation.PopAsync();
        }

        private void BtnCancelarEdicao_Clicked(object sender, EventArgs e)
        {
            tabDivisor.IsVisible = false;
            lblEditarDados.IsVisible = false;
            StackEditarDados.IsVisible = false;
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    btnConfirmar.IsVisible = true;

        //    entryEmail.Text = Utils.Settings.GeneralSettingsAccountEmail;
        //    entryTelefone.Text = Utils.Settings.GeneralSettingsAccountTelefone;

        //    if (editTelefone.IsVisible == true || editEmail.IsVisible == true)
        //    {
        //        editTelefone.IsVisible = false;
        //        editEmail.IsVisible = false;
        //        btnConfirmar.IsVisible = false;
        //    }
        //    else
        //    {
        //        editTelefone.IsVisible = true;
        //        editEmail.IsVisible = true;
        //    }
        //}
    }
}