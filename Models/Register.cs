using System.ComponentModel.DataAnnotations;

namespace FinalProjectAnimalShop.Models;

public class Register
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
    public string? LastName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(30)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [StringLength(30)]
    public string? ConfirmPassword { get; set; }
}
