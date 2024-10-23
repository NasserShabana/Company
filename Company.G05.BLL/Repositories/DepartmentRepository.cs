using Company.G05.BLL.Interfaces;
using Company.G05.DAL.Data.Contexts;
using Company.G05.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.BLL.Repositories
{
    public class DepartmentRepository : GenericReposoitory<Department>,IDepartmentRepository
    {

        public DepartmentRepository(AppDbContext Context) : base(Context) { }


        


    }
}
