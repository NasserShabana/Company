using Company.G05.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.ViewModels.Employees
{
    public class CreateEmployeeViewModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Range(20, 60, ErrorMessage = "Age Must be between 20 , 60")]
        public int? Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address Must be 123-street-city-country")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; } //Navigational Property - Optional


    }
}
