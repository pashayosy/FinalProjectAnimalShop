using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectAnimalShop.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsApproved { get; set; } = false;

    public string? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public virtual ApplicationUser? Author { get; set; }
}
