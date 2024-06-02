using Microsoft.EntityFrameworkCore;

namespace FinalProjectAnimalShop.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }

    //XXX: Can be removed in the future
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Animal>()
    //         .HasOne(a => a.Category)
    //         .WithMany(c => c.Animals)
    //         .HasForeignKey(a => a.CategoryId);

    //     modelBuilder.Entity<Comment>()
    //         .HasOne(c => c.Animal)
    //         .WithMany(a => a.Comments)
    //         .HasForeignKey(c => c.AnimalId);
    // }
}

