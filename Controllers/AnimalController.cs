using FinalProjectAnimalShop.Models;
using FinalProjectAnimalShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop;

public class AnimalController : Controller
{
    private ApplicationDbContext _context;
    private static int _animalId = -1;

    public AnimalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Details(int id)
    {
        var animal = _context.Animals
            .Include(a => a.Comments)
            .FirstOrDefault(a => a.AnimalId == id);

        if (animal == null)
        {
            return NotFound();
        }
        _animalId = animal.AnimalId;

        return View(animal);
    }


    public async Task<IActionResult> AddComment(string text)
    {
        if (ModelState.IsValid)
        {
            var animal = await _context.Animals
                .Include(a => a.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AnimalId == _animalId);

            if (animal == null)
            {
                return NotFound();
            }

            // Attach the animal and category as unchanged to avoid re-saving them
            _context.Entry(animal).State = EntityState.Unchanged;
            _context.Entry(animal.Category).State = EntityState.Unchanged;

            var comment = new Comment(text, _animalId, animal);

            _context.Comments.Add(comment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return View("Details", animal);
            }

            return RedirectToAction(nameof(Details), new { id = _animalId });
        }

        var animalWithComments = await _context.Animals
            .Include(a => a.Comments)
            .FirstOrDefaultAsync(m => m.AnimalId == _animalId);

        if (animalWithComments == null)
        {
            return NotFound();
        }
        // Return the details view with the validation errors
        return View("Details", animalWithComments);
    }
}

