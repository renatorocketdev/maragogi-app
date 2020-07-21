using AppTesteBinding.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using AppTesteBinding.Service.Modulo;

namespace AppTesteBinding.Service
{
    public class DataService
    {
        public HttpClient HttpClient;
        
        public string ApiBaseAddress = "https://ocaribedemaragogi.com.br/api/";

        public DataService()
        {
            HttpClient = CreateClient();
        }

        public HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        //public async Task<List<CategoriaMaragogi>> GetCategoriasMaragogi()
        //{
        //    using (var httpClient = CreateClient())
        //    {
        //        var response = await httpClient.GetAsync(ApiBaseAddress + "APICategoriasMaragogi/").ConfigureAwait(false);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        //            if (!string.IsNullOrWhiteSpace(json))
        //                return JsonConvert.DeserializeObject<List<CategoriaMaragogi>>(json);
        //        }

        //        return null;
        //    }

        //}


        //HttpClient client = new HttpClient();
        //public async Task<List<Categorias>> GetGateriasAPI()
        //{
        //    try
        //    {
        //        string Url = "https://ocaribedemaragogi.com.br/api/APICategorias";
        //        var Response = await client.GetStringAsync(Url);
        //        var Categorias = JsonConvert.DeserializeObject<List<Categorias>>(Response);
        //        return Categorias;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<Categorias>> GetGateriasMaragogiAPI()
        //{
        //    try
        //    {
        //        string Url = "https://ocaribedemaragogi.com.br/api/APICategoriasMaragogi";
        //        var Response = await client.GetStringAsync(Url);
        //        var Categorias = JsonConvert.DeserializeObject<List<Categorias>>(Response);
        //        return Categorias;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<Comentario>> GetComentariosCategoria(string filtro)
        //{
        //    try
        //    {
        //            string url = string.Format("https://www.appmaragogi.com.br/api/comentarios?{0}", filtro);
        //            var response = await client.GetStringAsync(url);
        //            var comentarios = JsonConvert.DeserializeObject<List<Comentario>>(response);
        //            return comentarios;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<List<Empresa>> GetEmpresas(string filtro)
        //{
        //    try
        //    {
        //        string url = "https://www.appmaragogi.com.br/api/Empresas?" + filtro;
        //        var response = await client.GetStringAsync(url);
        //        var empresas = JsonConvert.DeserializeObject<List<Empresa>>(response);
        //        return empresas;
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        //        return null;
        //    }
        //}

        //public async Task<List<Empresa>> GetEmpresaDetalhes(string filtro)
        //{
        //    try
        //    {
        //        string url = "https://www.appmaragogi.com.br/api/Empresas?NomeEmpresa=" + filtro;
        //        var response = await client.GetStringAsync(url);
        //        var empresas = JsonConvert.DeserializeObject<List<Empresa>>(response);
        //        return empresas;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task PostComentario(Comentario comentario)
        //{
        //    var uri = new Uri(string.Format("https://appmaragogi.com.br/api/Comentarios/{0}", string.Empty));

        //    var json = JsonConvert.SerializeObject(comentario);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = null;

        //    response = await client.PostAsync(uri, content);
        //}
    }
}