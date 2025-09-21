using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class Rating
	{
		public int Id { get; set; }

		[Required]
		public string PatientName { get; set; }

		[Required]
		public string DoctorName { get; set; }

		[Range(1, 5)]
		public int Stars { get; set; }

		public string? Comment { get; set; }

		public DateTime DateRated { get; set; } = DateTime.Now;
	}
}
