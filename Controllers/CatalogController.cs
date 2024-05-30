using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop;

public class CatalogController : Controller
{
    private readonly ApplicationDbContext _context;

    public CatalogController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories
                                 .Include(c => c.Animals)
                                 .ToList();
        return View(categories);
    }
}

