using AppTesteBinding.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTesteBinding.Data
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Categoria>().Wait();
            _database.CreateTableAsync<CategoriaEnglish>().Wait();
            _database.CreateTableAsync<CategoriaMaragogi>().Wait();
            _database.CreateTableAsync<Empresa>().Wait();
            _database.CreateTableAsync<HistoriaMaragogi>().Wait();
        }

        //public void Delete<T>(string filter = "")
        //{
        //    try
        //    {
        //        string sql;
        //        if (string.IsNullOrEmpty(filter))
        //        {
        //            sql = $"DELETE FROM {typeof(T).Name}";
        //        }
        //        else
        //        {
        //            sql = $"DELETE FROM {typeof(T).Name} WHERE MainCategoria = '{filter}'";
        //        }

        //        _database.ExecuteAsync(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //    }
        //}

        //public void Save<T>(IEnumerable<T> values)
        //{
        //    try
        //    {
        //        _database.InsertAllAsync(values);
        //    }
        //    catch (Exception ex)
        //    {
        //        Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //    }
        //}

        //public async Task<List<CategoriaEnglish>> GetCategoriesEnglishList(string filtro)
        //{
        //    try
        //    {
        //        return await _database.QueryAsync<CategoriaEnglish>($"SELECT * FROM CategoriaEnglish WHERE MainCategoria = '{filtro}'");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //        return null;
        //    }
        //}

        //public async Task<List<Categoria>> GetCategoriesList(string filtro)
        //{
        //    try
        //    {
        //        return await _database.QueryAsync<Categoria>($"SELECT DISTINCT * FROM Categoria WHERE MainCategoria = '{filtro}'");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //        return null;
        //    }
        //}

        //public async Task<IEnumerable<CategoriaMaragogi>> GetCategoriesMaragogiList()
        //{
        //    try
        //    {
        //        return await _database.QueryAsync<CategoriaMaragogi>($"SELECT * FROM CategoriaMaragogi");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //        return null;
        //    }
        //}

        //public async Task<List<Empresa>> GetEnterpriseList(string filtro)
        //{
        //    try
        //    {
        //        return await _database.QueryAsync<Empresa>($"SELECT * FROM Empresa WHERE SubCategoria  = '{filtro}'");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //        return new List<Empresa>();
        //    }
        //}

        //public Task<List<Empresa>> GetEnterpriseByName(string filtro)
        //{
        //    try
        //    {
        //        return _database.QueryAsync<Empresa>($"SELECT * FROM Empresa WHERE NomeEmpresa  = '{filtro}'");
        //    }
        //    catch (Exception ex)
        //    {
        //        Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //        return null;
        //    }
        //}

        //public async Task<HistoriaMaragogi> GetHistoriaMaragogi()
        //{
        //    try
        //    {
        //        var result = await _database.QueryAsync<HistoriaMaragogi>($"SELECT * FROM HistoriaMaragogi");

        //        if (result != null)
        //        {
        //            return result.FirstOrDefault();
        //        }
        //        else
        //        {
        //            return new HistoriaMaragogi();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //        return null;
        //    }
        //}

        //#region Check if has something

        //public bool HasCategories(string pagina)
        //{
        //    var result = _database.QueryAsync<Categoria>($"SELECT * FROM Categoria WHERE MainCategoria = '{pagina}'").Result;

        //    if (result.Count == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //public bool HasCategoriesEnglish(string pagina)
        //{
        //    var result = _database.QueryAsync<CategoriaEnglish>($"SELECT * FROM CategoriaEnglish WHERE MainCategoria = '{pagina}'").Result;

        //    if (result.Count == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //public bool HasEnterprises(string SubCategoria)
        //{
        //    var result = _database.QueryAsync<Empresa>($"SELECT * FROM Empresa WHERE SubCategoria = '{SubCategoria}'").Result;

        //    if (result.Count == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //#endregion Check if has something

        //public void SaveHistoriaMaragogi(HistoriaMaragogi historiaMaragogi)
        //{
        //    try
        //    {
        //        _database.InsertAsync(historiaMaragogi);
        //    }
        //    catch (Exception ex)
        //    {
        //        Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
        //    }
        //}
    }
}