using AppTesteBinding.Models;
using AppTesteBinding.Utils;
using AppTesteBinding.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesteBinding.View.MenuPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTeste : ContentPage
    {
        public static string filtro;
        string Fil;
        string Emp;
        string Nom;
        string Cat;
        DataService dataService;

        public PageTeste(string filtro, string empresa, string nome, string categoria)
        {
            InitializeComponent();

            var current = Connectivity.NetworkAccess;
            if(current == NetworkAccess.Internet)
            {
                PageTeste.filtro = filtro;
                Fil = filtro;
                Emp = empresa;
                Nom = nome;
                Cat = categoria;
                dataService = new DataService();
                AtualizaDados();
            }

            var trg = new TapGestureRecognizer();
            trg.Tapped += (s, e) => OnLabelClicked();
            lblVoltar.GestureRecognizers.Add(trg);
        }

        private void OnLabelClicked()
        {
            Navigation.PopAsync();
        }

        List<Comentario> comentario;

        async void AtualizaDados()
        {
            actInd.IsRunning = true;

            //comentario = await dataService.GetComentariosCategoria(filtro);
            //listaProdutos.ItemsSource = comentario.OrderBy(item => item.Nome).ToList();

            //if (comentario.Count == 0)
            //{
            //    actInd.IsRunning = false;
            //    NenhumComentario.IsVisible = true;
            //}
            //else
            //{
            //    actInd.IsRunning = false;
            //}
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Valida())
            {

                if(Settings.Logado)
                {
                    Comentario novoComentario = new Comentario
                    {
                        Nome = Nom,
                        Empresa = Emp,
                        Categoria = Cat,
                        Texto = txtTexto.Text.Trim(),
                        Nota = Convert.ToDecimal(txtNota.SelectedItem)
                    };
                    try
                    {
                        //await dataService.PostComentario(novoComentario);
                        LimpaProduto();
                        AtualizaDados();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Erro", ex.Message, "OK");
                    }
                }else
                {
                    await DisplayAlert("Erro", "Você não pode avaliar sem cadastro...", "OK");
                }
                 
            }
            else
            {
                await DisplayAlert("Erro", "Dados inválidos...", "OK");
            }
        }

        private bool Valida()
        {
            if (string.IsNullOrEmpty(txtTexto.Text) || txtNota.SelectedItem == null)
                return false;
            else
                return true;
        }

        private void LimpaProduto()
        {
            txtTexto.Text = "";
            txtNota.SelectedItem = -1;
        }
    }
}