namespace FinalProjectAnimalShop;

using System.Security.Claims;
using FinalProjectAnimalShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // [HttpPost]
    // public async Task<IActionResult> Login(Login model)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
    //         if (result.Succeeded)
    //         {
    //             return RedirectToAction("Index", "Home");
    //         }
    //         ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //     }
    //     return View(model);
    // }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Register model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.FirstName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            bool userExists = await _userManager.FindByEmailAsync(model.Email) != null;
            if (!userExists)
            {
                var userCreateresult = await _userManager.CreateAsync(user, model.Password);
                var roleAddresult = await _userManager.AddToRolesAsync(user, ["Regular"]);

                if (userCreateresult.Succeeded && roleAddresult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                IEnumerable<IdentityError> errors = userCreateresult.Errors.Concat(roleAddresult.Errors);
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ModelState.AddModelError(string.Empty, "Please use other email!");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Manage()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var model = new Manage
        {
            Email = user.Email
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Manage(Manage model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var email = user.Email;
        if (model.Email != email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
            if (!setEmailResult.Succeeded)
            {
                foreach (var error in setEmailResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
        {
            if (model.NewPassword != model.OldPassword)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The entered password were used before");
                return View(model);
            }

        }
        await _signInManager.RefreshSignInAsync(user);
        ViewData["Message"] = "Your profile has been updated";
        return RedirectToAction("Index", "Home");
    }
}