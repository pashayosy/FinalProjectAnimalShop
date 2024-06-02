using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var animals = _context.Animals.Include(a => a.Category).ToList();
        return View(animals);
    }

    public IActionResult Create()
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Animal animal, IFormFile photo)
    {
        ModelState.Remove(nameof(photo));

        if (ModelState.IsValid)
        {

            if (photo != null && photo.Length > 0)
            {
                // Generate a unique file name to prevent overwriting
                var fileName = Path.GetFileNameWithoutExtension(photo.FileName);
                var extension = Path.GetExtension(photo.FileName);
                var uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                // Save the file path in the database
                animal.PictureUrl = $"/img/{uniqueFileName}";
            }
            else if (string.IsNullOrEmpty(animal.PictureUrl))
            {
                // Assign default image if no file was uploaded and no URL was provided
                animal.PictureUrl = "/img/NoImage.jpg";
            }

            _context.Animals.Add(animal);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name", animal.CategoryId);
        return View(animal);
    }

    // Edit and Delete actions...

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name", animal.CategoryId);
        return View(animal);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, IFormFile photo)
    {
        ModelState.Remove(nameof(photo));
        var animalToUpdate = await _context.Animals.FindAsync(id);
        if (animalToUpdate == null)
        {
            return NotFound();
        }

        if (photo != null && photo.Length > 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(photo.FileName);
            var extension = Path.GetExtension(photo.FileName);
            var uniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            animalToUpdate.PictureUrl = $"/img/{uniqueFileName}";
        }

        if (await TryUpdateModelAsync<Animal>(animalToUpdate, "", a => a.Name, a => a.Age, a => a.Description, a => a.CategoryId, a => a.PictureUrl))
        {
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }

        ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name", animalToUpdate.CategoryId);
        return View(animalToUpdate);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var animal = await _context.Animals
            .Include(a => a.Category)
            .FirstOrDefaultAsync(m => m.AnimalId == id);
        if (animal == null)
        {
            return NotFound();
        }
        return View(animal);
    }


    public async Task<IActionResult> DeleteConfirmed(int AnimalId)
    {
        var animal = await _context.Animals.FindAsync(AnimalId);
        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


}

