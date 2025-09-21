using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediTechHospital.Data;
using MediTechHospital.Models;

namespace MediTechHospital.Controllers
{
	public class ClinicsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ClinicsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index() => View(await _context.Clinics.ToListAsync());

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(Clinic clinic)
		{
			if (!ModelState.IsValid) return View(clinic);
			_context.Clinics.Add(clinic);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var clinic = await _context.Clinics.FindAsync(id);
			return clinic == null ? NotFound() : View(clinic);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Clinic clinic)
		{
			if (!ModelState.IsValid) return View(clinic);
			_context.Update(clinic);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int id)
		{
			var clinic = await _context.Clinics.FindAsync(id);
			return clinic == null ? NotFound() : View(clinic);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var clinic = await _context.Clinics.FindAsync(id);
			return clinic == null ? NotFound() : View(clinic);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var clinic = await _context.Clinics.FindAsync(id);
			if (clinic == null)
				return NotFound();

			_context.Clinics.Remove(clinic);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

	}
}
