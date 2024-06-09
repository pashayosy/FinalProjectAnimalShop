using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinalProjectAnimalShop;

[Authorize]
public class PostController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PostController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = "Postmen")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Postmen")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        post.AuthorId = _userManager.GetUserId(User);
        if (ModelState.IsValid)
        {
            post.CreatedAt = DateTime.Now;
            _context.Add(post);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Post created successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> Approve(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        post.IsApproved = true;
        _context.Update(post);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Post approved successfully\nWaiting for Approval";
        return RedirectToAction(nameof(Manage));
    }

    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> Manage()
    {
        var posts = await _context.Posts.Include(p => p.Author).ToListAsync();
        return View(posts);
    }

    [Authorize(Roles = "Admin, Moderator, Postmen")]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null || post.AuthorId != _userManager.GetUserId(User) && !(User.IsInRole("Admin") || User.IsInRole("Moderator")))
        {
            return Forbid();
        }
        return View(post);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Moderator, Postmen")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Post post)
    {
        if (id != post.PostId)
        {
            return NotFound();
        }

        var postToUpdate = await _context.Posts.FindAsync(id);
        if (postToUpdate.AuthorId != _userManager.GetUserId(User) && !(User.IsInRole("Admin") || User.IsInRole("Moderator")))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            _context.Update(postToUpdate);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Post edited successfully";
            return RedirectToAction(GetRediracByRole());
        }
        return View(post);
    }

    private string GetRediracByRole()
    {
        return User.IsInRole("Postmen") ? "Index" : "Manage";
    }

    [Authorize(Roles = "Admin, Moderator, Postmen")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        if (post.AuthorId != _userManager.GetUserId(User) && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
        {
            return Forbid();
        }
        return View(post);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Moderator, Postmen")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int PostId)
    {
        var post = await _context.Posts.FindAsync(PostId);

        if (post == null)
            return NotFound();

        if (post.AuthorId != _userManager.GetUserId(User) && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
        {
            return Forbid();
        }
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Post deleted successfully";
        return RedirectToAction(GetRediracByRole());
    }

    public async Task<IActionResult> Index()
    {
        var posts = await _context.Posts.Include(p => p.Author).Where(p => p.IsApproved).ToListAsync();
        return View(posts);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }
        ViewBag.UserId = _userManager.GetUserId(User);
        return View(post);
    }
}
