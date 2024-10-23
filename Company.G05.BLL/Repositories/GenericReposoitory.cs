using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Data.Contexts;
using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Repositories
{
    public class GenericReposoitory<T> : IGenericReposoitory<T> where T : BaseEntity
    {
        private readonly protected AppDbContext _Context;
        public GenericReposoitory(AppDbContext dbContext) { 
        _Context = dbContext;
        }
        public async Task<int> AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            return  _Context.SaveChanges();
        }

        public async Task<int> Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return await _Context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(int? id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {  
            if(typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>)await _Context.Employees.Include(E=>E.WorkFor).ToListAsync();

            }
            return await _Context.Set<T>().ToListAsync();

        }

        public async Task<int> UpDate(T entity)
        {
            _Context.Set<T>().Update(entity);
            return await _Context.SaveChangesAsync();
        }
    }
}
 