using System.ComponentModel.DataAnnotations;

namespace FinalProjectAnimalShop.Models;

public class EditUser
{
    public string? Id { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    public IList<string>? UserRoles { get; set; }

    public IList<string>? AllRoles { get; set; }

    [Display(Name = "Selected Roles")]
    public List<string>? SelectedRoles { get; set; }
}
