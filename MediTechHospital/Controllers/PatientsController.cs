using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediTechHospital.Data;
using MediTechHospital.Models;

namespace MediTechHospital.Controllers
{
	public class PatientsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PatientsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Patients.ToListAsync());
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(Patient patient)
		{
			if (!ModelState.IsValid) return View(patient);
			_context.Patients.Add(patient);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var patient = await _context.Patients.FindAsync(id);
			return patient == null ? NotFound() : View(patient);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Patient patient)
		{
			if (!ModelState.IsValid) return View(patient);
			_context.Update(patient);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int id)
		{
			var patient = await _context.Patients.FindAsync(id);
			return patient == null ? NotFound() : View(patient);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var patient = await _context.Patients.FindAsync(id);
			return patient == null ? NotFound() : View(patient);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var patient = await _context.Patients.FindAsync(id);
			if (patient == null)
				return NotFound();

			_context.Patients.Remove(patient);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

	}
}
