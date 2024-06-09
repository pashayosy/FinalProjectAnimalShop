using Microsoft.AspNetCore.Identity;

namespace FinalProjectAnimalShop.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Post>? Posts { get; set; }
}
