using MediTechHospital.Data;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
	private readonly ApplicationDbContext _context;

	public DashboardController(ApplicationDbContext context)
	{
		_context = context;
	}


	public IActionResult Index()
	{
		ViewBag.DoctorsCount = _context.Doctors.Count();
		ViewBag.PatientsCount = _context.Patients.Count();
		ViewBag.ClinicsCount = _context.Clinics.Count();
		ViewBag.AppointmentsCount = _context.Appointments.Count();
		ViewBag.RatingsCount = _context.Ratings.Count();



		

		return View();
	}
}
