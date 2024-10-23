using Company.G05.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenericReposoitory<Employee>
    {
        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee Model);
        //int Update(Employee Model);
        //int Delete(Employee Model);
        Task<IEnumerable<Employee>> GetByNameAsync(string Name);


    }
}
