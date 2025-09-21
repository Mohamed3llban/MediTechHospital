using MediTechHospital.Data;
using MediTechHospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MediTechHospital.Controllers
{
	public class DoctorsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public DoctorsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Doctors.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Doctor doctor)
		{
			if (!ModelState.IsValid)
				return View(doctor);

			_context.Doctors.Add(doctor);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var doctor = await _context.Doctors.FindAsync(id);
			return doctor == null ? NotFound() : View(doctor);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Doctor doctor)
		{
			if (!ModelState.IsValid)
				return View(doctor);

			_context.Update(doctor);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int id)
		{
			var doctor = await _context.Doctors.FindAsync(id);
			return doctor == null ? NotFound() : View(doctor);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var doctor = await _context.Doctors.FindAsync(id);
			return doctor == null ? NotFound() : View(doctor);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var doctor = await _context.Doctors.FindAsync(id);
			if (doctor == null)
				return NotFound();

			_context.Doctors.Remove(doctor);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		

   
	
		
	
	}


}

