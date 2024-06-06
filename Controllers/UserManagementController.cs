namespace FinalProjectAnimalShop;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinalProjectAnimalShop.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin,Moderator")]
public class UserManagementController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        ViewBag.userManager = _userManager;
        return View(users);
    }

    [Authorize(Roles = "Moderator")]
    public IActionResult ModeratorView()
    {
        IList<ApplicationUser> users = _userManager.Users.ToList();

        users = users.Where(u => !_userManager.IsInRoleAsync(u, "Admin").Result).ToList();
        ViewBag.userManager = _userManager;
        return View("Index", users);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new EditUser
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserRoles = await _userManager.GetRolesAsync(user),
            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUser model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;

        var userRoles = await _userManager.GetRolesAsync(user);
        var selectedRoles = model.SelectedRoles ?? new List<string>();

        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to add roles.");
            return View(model);
        }

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to remove roles.");
            return View(model);
        }

        await _userManager.UpdateAsync(user);

        return RedirectToAction(nameof(Index));
    }
}
