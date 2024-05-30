using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication;

namespace FinalProjectAnimalShop.Models;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    [Required(ErrorMessage = "The Text field is required.")]
    [StringLength(500, ErrorMessage = "The Comment must be at most 500 characters long.")]
    public string? Text { get; set; }

    [Required(ErrorMessage = "The Animal field is required.")]
    public int AnimalId { get; set; }

    [ForeignKey("AnimalId")]
    public Animal? Animal { get; set; }


    public Comment(string? text, int animalId, Animal? animal)
    {
        Text = text;
        AnimalId = animalId;
        Animal = animal;
    }

    public Comment()
    {

    }

}

