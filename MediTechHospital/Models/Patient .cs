using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class Patient
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Full Name")]
		public string FullName { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		public string Gender { get; set; }

		[Phone]
		public string Phone { get; set; }

		[EmailAddress]
		public string Email { get; set; }
	}
}
