using FinalProjectAnimalShop.Models;
using FinalProjectAnimalShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public AdminController(ApplicationDbContext context, FileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public IActionResult Index()
    {
        var animals = _context.Animals.Include(a => a.Category).ToList();
        return View(animals);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Animal animal, IFormFile photo)
    {
        ModelState.Remove(nameof(photo));

        if (ModelState.IsValid)
        {

            if (photo != null && photo.Length > 0)
            {
                animal.PictureUrl = await _fileService.UploadFileAsync(photo);
            }
            else if (string.IsNullOrEmpty(animal.PictureUrl))
            {
                animal.PictureUrl = "/img/NoImage.jpg";
            }

            _context.Animals.Add(animal);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Animal added successfully.";
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
            animalToUpdate.PictureUrl = await _fileService.UploadFileAsync(photo);
        }

        if (await TryUpdateModelAsync<Animal>(animalToUpdate, "", a => a.Name, a => a.Age, a => a.Description, a => a.CategoryId, a => a.PictureUrl))
        {
            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Animal updated successfully.";
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

        _fileService.DeleteFile(animal.PictureUrl);

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Animal deleted successfully.";
        return RedirectToAction(nameof(Index));
    }


}

