using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
		[EmailAddress(ErrorMessage = "صيغة البريد غير صحيحة")]
		public string Email { get; set; }
	}
}
