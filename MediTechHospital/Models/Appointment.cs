using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediTechHospital.Models
{
	public class Appointment
	{
		public int Id { get; set; }

		[Required]
		public string PatientName { get; set; }

		[Required]
		public string DoctorName { get; set; }

		[Required]
		public DateTime AppointmentDate { get; set; }

		public string? Notes { get; set; }
	}
}
