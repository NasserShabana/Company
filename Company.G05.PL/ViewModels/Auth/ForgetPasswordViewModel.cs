using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.ViewModels.Auth
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email IS Required")]
		[EmailAddress(ErrorMessage = " Invalid Email !")]
		public string Email { get; set; }

	}
}
