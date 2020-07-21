using AppTesteBinding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppTesteBinding.ViewModels.Interfaces
{
    interface IManageEmpresa<T>
    {
        void GetAsync(string Filtro);
        void AddImagens(string Categoria);
        void GetNameCategories(string Categoria);
    }
}
