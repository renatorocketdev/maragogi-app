using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppTesteBinding.Service
{
    interface IService<T>
    {
       Task<List<T>> Get();
    }
}
