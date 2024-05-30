using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop;

public class AnimalController : Controller
{
    private readonly ApplicationDbContext _context;

    public AnimalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Details(int id)
    {
        var animal = _context.Animals
            .Include(a => a.Comments)
            .Include(a => a.Category)
            .FirstOrDefault(a => a.AnimalId == id);

        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }
}

