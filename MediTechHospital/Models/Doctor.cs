using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class Doctor
	{
		public int Id { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public string Specialty { get; set; }

		public string Phone { get; set; }

		[EmailAddress]
		public string Email { get; set; }
	}
}
