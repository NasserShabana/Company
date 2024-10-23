using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String  FirstName { get; set; }
        public String LastName { get; set; }
        public bool IsAgree {get; set; }


    }
}
