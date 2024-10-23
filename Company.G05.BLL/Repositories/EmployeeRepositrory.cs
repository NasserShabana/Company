using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Data.Contexts;
using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Repositories
{
    public class EmployeeRepositrory : GenericReposoitory<Employee>, IEmployeeRepository
    {
        public EmployeeRepositrory(AppDbContext Context):base(Context) { }

        public async Task<IEnumerable<Employee>>  GetByNameAsync(string Name)
        {
            return await _Context.Employees.Where(E => E.Name.ToLower().Contains(Name.ToLower())).ToListAsync();
        }
    }

}
    

