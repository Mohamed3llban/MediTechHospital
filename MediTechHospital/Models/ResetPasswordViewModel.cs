using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class ResetPasswordViewModel
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Token { get; set; }

		[Required(ErrorMessage = "كلمة المرور مطلوبة")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
		public string ConfirmPassword { get; set; }
	}
}
