using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.ViewModels.Auth
{
	public class SignUPViewModel
	{
        [Required(ErrorMessage ="UserName IS Required")]
        public string UserName { get; set; }


		[Required(ErrorMessage = "FirstName IS Required")]
		public string FirstName { get; set; }


		[Required(ErrorMessage = "LastName IS Required")]
		public string LastName { get; set; }


		[Required(ErrorMessage = "Email IS Required")]
		[EmailAddress(ErrorMessage =" Invalid Email !")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password IS Required")]
		[DataType(DataType.Password)]
		public string Password   { get; set; }


		[Required(ErrorMessage = "ConfirmPassword IS Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Could't Create Your account Without Your agree :( ")]
		public bool IsAgree { get; set; }
    }
}
