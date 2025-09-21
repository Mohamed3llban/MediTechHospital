using System.ComponentModel.DataAnnotations;

namespace MediTechHospital.Models
{
	public class Clinic
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Clinic Name")]
		public string Name { get; set; }

		[Display(Name = "Location")]
		public string Location { get; set; }

		[Display(Name = "Opening Hours")]
		public string OpeningHours { get; set; }
	}
}
