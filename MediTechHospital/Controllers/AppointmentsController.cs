using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediTechHospital.Data;
using MediTechHospital.Models;

namespace MediTechHospital.Controllers
{
	public class AppointmentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AppointmentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Appointments
		public async Task<IActionResult> Index()
		{
			var appointments = await _context.Appointments.ToListAsync();
			return View(appointments);
		}

		// GET: Appointments/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Appointments/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Appointment appointment)
		{
			if (ModelState.IsValid)
			{
				_context.Appointments.Add(appointment);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(appointment);
		}

		// GET: Appointments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment == null) return NotFound();

			return View(appointment);
		}

		// POST: Appointments/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Appointment appointment)
		{
			if (id != appointment.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(appointment);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AppointmentExists(appointment.Id))
						return NotFound();
					else
						throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(appointment);
		}

		// GET: Appointments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
			if (appointment == null) return NotFound();

			return View(appointment);
		}

		// POST: Appointments/DeleteConfirmed
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment != null)
			{
				_context.Appointments.Remove(appointment);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}

		private bool AppointmentExists(int id)
		{
			return _context.Appointments.Any(e => e.Id == id);
		}
	}
}
