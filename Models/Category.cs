using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectAnimalShop.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name must be at most 100 characters long.")]
    public string? Name { get; set; }

    // Navigation Property
    public virtual ICollection<Animal>? Animals { get; set; }

    public Category(string? name, ICollection<Animal>? animals)
    {
        Name = name;
        Animals = animals;
    }

    public Category()
    {

    }

}
