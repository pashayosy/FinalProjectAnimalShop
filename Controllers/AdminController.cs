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
    public IActionResult Create(Animal animal)
    {
        if (ModelState.IsValid)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name", animal.CategoryId);
        return View(animal);
    }

    // Edit and Delete actions...
}

