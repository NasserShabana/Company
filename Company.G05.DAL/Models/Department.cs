using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G05.DAL.Models
{
    public class Department : BaseEntity
    {
        [Required (ErrorMessage = "Name Is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Is required")]
        public string Code { get; set; }
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee>? Employees { get; set; }

    }
}
