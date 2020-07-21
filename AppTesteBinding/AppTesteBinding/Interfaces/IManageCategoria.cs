using AppTesteBinding.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppTesteBinding.ViewModels.Interfaces
{
    public interface IManageCategoria<T>
    {
        Task<IEnumerable<Categoria>> TraslateList(List<Categoria> list);
        void GetNameCategorie();
        void AddImagens();
        void GetAsync();
        
    }
}
