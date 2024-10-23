using System.ComponentModel.DataAnnotations;

namespace Company.G05.PL.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{


		[Required(ErrorMessage = "Password IS Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "ConfirmPassword IS Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}
