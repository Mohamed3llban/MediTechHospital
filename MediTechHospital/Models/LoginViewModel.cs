using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
		[EmailAddress(ErrorMessage = "صيغة البريد غير صحيحة")]
		public string Email { get; set; }

		[Required(ErrorMessage = "كلمة المرور مطلوبة")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "تذكرني")]
		public bool RememberMe { get; set; }
	}
}
