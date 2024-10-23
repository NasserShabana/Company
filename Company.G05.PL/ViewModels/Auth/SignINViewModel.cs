using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.ViewModels.Auth
{
	public class SignINViewModel
	{
		[Required(ErrorMessage = "Email IS Required")]
		[EmailAddress(ErrorMessage = " Invalid Email !")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password IS Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
