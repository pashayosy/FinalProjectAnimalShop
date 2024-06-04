using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var topAnimals = _context.Animals
                    .Include(a => a.Comments)
                    .OrderByDescending(a => a.Comments.Count)
                    .Take(2)
                    .ToList();

        return View(topAnimals);
    }
}

