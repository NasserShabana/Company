using Company.G05.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Interfaces
{
    public interface IGenericReposoitory<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        Task<int> AddAsync(T entity);
        Task<int> UpDate(T entity);
        Task<int> Delete(T entity);
    }
}
