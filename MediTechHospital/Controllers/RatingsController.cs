using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediTechHospital.Data;
using MediTechHospital.Models;

namespace MediTechHospital.Controllers
{
	public class RatingsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public RatingsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _context.Ratings.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Rating rating)
		{
			if (ModelState.IsValid)
			{
				_context.Add(rating);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(rating);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var rating = await _context.Ratings.FindAsync(id);
			if (rating == null) return NotFound();

			return View(rating);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Rating rating)
		{
			if (id != rating.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(rating);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RatingExists(rating.Id))
						return NotFound();
					else
						throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(rating);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var rating = await _context.Ratings.FindAsync(id);
			if (rating != null)
			{
				_context.Ratings.Remove(rating);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		private bool RatingExists(int id)
		{
			return _context.Ratings.Any(e => e.Id == id);
		}
	}
}
