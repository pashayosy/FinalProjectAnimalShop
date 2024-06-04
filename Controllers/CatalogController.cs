using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FinalProjectAnimalShop;

public class CatalogController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly int _pageSize = 9;

    public CatalogController(ApplicationDbContext context)
    {
        _context = context;
    }

    // public IActionResult Index()
    // {
    //     var categories = _context.Categories
    //                              .Include(c => c.Animals)
    //                              .ToList();
    //     return View(categories);
    // }

    // [HttpGet]
    // public async Task<IActionResult> Index(string searchTerm, int? page)
    // {
    //     var animals = _context.Animals.Include(a => a.Category).AsQueryable();

    //     if (!string.IsNullOrEmpty(searchTerm))
    //     {
    //         animals = animals.Where(a => a.Name.Contains(searchTerm));
    //     }
    //     int pageNumber = page ?? 1;

    //     var pagedAnimals = await animals.ToPagedListAsync(pageNumber, _pageSize);

    //     ViewBag.SearchTerm = searchTerm;

    //     return View(pagedAnimals);
    // }

    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm, int? categoryId, int page = 1)
    {
        var animals = _context.Animals.Include(a => a.Category).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            animals = animals.Where(a => a.Name!.Contains(searchTerm));
        }

        if (categoryId.HasValue && categoryId > 0)
        {
            animals = animals.Where(a => a.CategoryId == categoryId);
        }

        var pagedAnimals = await animals.ToPagedListAsync(page, _pageSize);

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", categoryId);

        ViewBag.SearchTerm = searchTerm;
        ViewBag.CategoryId = categoryId;

        return View(pagedAnimals);
    }


}

