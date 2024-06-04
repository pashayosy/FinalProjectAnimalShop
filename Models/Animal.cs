using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectAnimalShop.Models;

public class Animal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnimalId { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(100, ErrorMessage = "The Name must be at most 100 characters long.")]
    public string? Name { get; set; }

    [Range(0, 100, ErrorMessage = "Age must be between 0 and 100.")]
    public int Age { get; set; }

    //[Required(ErrorMessage = "The Picture URL field is required.")]
    //[Url(ErrorMessage = "The Picture URL must be a valid URL.")]
    public string? PictureUrl { get; set; }

    [StringLength(500, ErrorMessage = "The Description must be at most 500 characters long.")]
    public string? Description { get; set; }

    // Foreign Key
    [Required(ErrorMessage = "The Category field is required.")]
    public int CategoryId { get; set; }

    // Navigation Properties
    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }

    public Animal(string? name, int age, string? pictureUrl, string? description, int categoryId, Category? category, ICollection<Comment>? comments)
    {
        Name = name;
        Age = age;
        PictureUrl = pictureUrl;
        Description = description;
        CategoryId = categoryId;
        Category = category;
        Comments = comments;
    }

    public Animal()
    {
    }
}
