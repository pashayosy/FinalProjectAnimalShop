using System.ComponentModel.DataAnnotations;

namespace FinalProjectAnimalShop.Models;
public class Manage
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? OldPassword { get; set; }

    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}
