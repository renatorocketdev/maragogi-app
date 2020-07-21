using AppTesteBinding.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
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
            _database.CreateTableAsync<Praias>().Wait();
        }

        #region Delete

        public void DeleteCategoriasMaragogi()
        {
            try
            {
                _database.ExecuteAsync($"DELETE FROM CategoriaMaragogi");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void DeleteCategories(string Categoria)
        {
            try
            {
                _database.ExecuteAsync($"DELETE FROM Categoria WHERE MainCategoria = '{Categoria}'");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void DeleteCategoriesEnglish(string Categoria)
        {
            try
            {
                _database.ExecuteAsync($"DELETE FROM CategoriaEnglish WHERE MainCategoria = '{Categoria}'");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void DeletePraias()
        {
            try
            {
                _database.ExecuteAsync($"DELETE FROM Praias");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void DeleteHistoriaMaragogi()
        {
            try
            {
                _database.ExecuteAsync($"DELETE FROM HistoriaMaragogi");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        #endregion Delete

        #region GetList

        public async Task<List<CategoriaEnglish>> GetCategoriesEnglishList(string filtro)
        {
            try
            {
                return await _database.QueryAsync<CategoriaEnglish>($"SELECT * FROM CategoriaEnglish WHERE MainCategoria = '{filtro}'");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return null;
            }
        }

        public async Task<List<Categoria>> GetCategoriesList(string filtro)
        {
            try
            {
                return await _database.QueryAsync<Categoria>($"SELECT DISTINCT * FROM Categoria WHERE MainCategoria = '{filtro}'");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return null;
            }
        }

        public async Task<List<CategoriaMaragogi>> GetCategoriesMaragogiList()
        {
            try
            {
                var result = await _database.QueryAsync<CategoriaMaragogi>($"SELECT * FROM CategoriaMaragogi");
                
                if(result != null)
                {
                    return result;
                }
                else
                {
                    return new List<CategoriaMaragogi>();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return new List<CategoriaMaragogi>();
            }
        }

        public async Task<List<Empresa>> GetEnterpriseList(string filtro)
        {
            try
            {
                return await _database.QueryAsync<Empresa>($"SELECT * FROM Empresa WHERE SubCategoria  = '{filtro}'");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return new List<Empresa>();
            }
        }

        public async Task<List<Praias>> GetPraiasList()
        {
            try
            {
                var result = await _database.QueryAsync<Praias>($"SELECT * FROM Praias"); ;

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<Praias>();
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return null;
            }
        }

        #endregion GetList

        #region Get

        public Task<List<Empresa>> GetEnterpriseByName(string filtro)
        {
            try
            {
                return _database.QueryAsync<Empresa>($"SELECT * FROM Empresa WHERE NomeEmpresa  = '{filtro}'");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return null;
            }
        }

        public async Task<HistoriaMaragogi> GetHistoriaMaragogi()
        {
            try
            {
                var result = await _database.QueryAsync<HistoriaMaragogi>($"SELECT * FROM HistoriaMaragogi");

                if (result != null)
                {
                    return result.FirstOrDefault();
                }
                else
                {
                    return new HistoriaMaragogi();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return null;
            }
        }

        public async Task<Praias> GetPraiasByName(string Nome)
        {
            try
            {
                var result = await _database.QueryAsync<Praias>($"SELECT * FROM Praias WHERE Nome = '{Nome}'");
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
                return null;
            }
        }

        #endregion Get

        #region Check if has something

        public bool HasCategories(string pagina)
        {
            var result = _database.QueryAsync<Categoria>($"SELECT * FROM Categoria WHERE MainCategoria = '{pagina}'").Result;

            if (result.Count == 0)
                return false;
            else
                return true;
        }

        public bool HasCategoriesEnglish(string pagina)
        {
            var result = _database.QueryAsync<CategoriaEnglish>($"SELECT * FROM CategoriaEnglish WHERE MainCategoria = '{pagina}'").Result;

            if (result.Count == 0)
                return false;
            else
                return true;
        }

        public bool HasEnterprises(string SubCategoria)
        {
            var result = _database.QueryAsync<Empresa>($"SELECT * FROM Empresa WHERE SubCategoria = '{SubCategoria}'").Result;

            if (result.Count == 0)
                return false;
            else
                return true;
        }

        public bool HasPraias()
        {
            var result = _database.QueryAsync<Praias>($"SELECT * FROM Praias").Result;

            if (result.Count == 0)
                return false;
            else
                return true;
        }

        #endregion Check if has something

        #region Save To Database

        public void SaveCategoriasMaragogi(IEnumerable<CategoriaMaragogi> list)
        {
            try
            {
                _database.InsertAllAsync(list);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void SaveCategories(IEnumerable<Categoria> list)
        {
            try
            {
                _database.InsertAllAsync(list);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void SaveCategoriesEnglish(IEnumerable<CategoriaEnglish> list)
        {
            try
            {
                _database.InsertAllAsync(list);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void SaveEnterprises(IEnumerable<Empresa> list)
        {
            try
            {
                _database.InsertAllAsync(list);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void SaveHistoriaMaragogi(HistoriaMaragogi historiaMaragogi)
        {
            try
            {
                _database.InsertAsync(historiaMaragogi);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void SavePraias(IEnumerable<Praias> list)
        {
            try
            {
                _database.InsertAllAsync(list);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        #endregion Save To Database
    }
}