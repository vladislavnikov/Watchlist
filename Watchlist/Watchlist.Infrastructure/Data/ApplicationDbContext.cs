using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watchlist.Infrastructure.Data.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }
    public DbSet<UserShow> UserShows { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MediaItem>()
        .UseTptMappingStrategy();

        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<Show>().ToTable("Shows");

        modelBuilder.Entity<MediaItem>()
            .HasOne(mi => mi.Director)
            .WithMany(d => d.MediaItems)
            .HasForeignKey(mi => mi.DirectorId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<UserMovie>()
            .HasKey(um => new { um.UserId, um.MovieId });

        modelBuilder.Entity<UserMovie>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMovies)
            .HasForeignKey(um => um.UserId);

        modelBuilder.Entity<UserMovie>()
            .HasOne(um => um.Movie)
            .WithMany(m => m.UserMovies)
            .HasForeignKey(um => um.MovieId);

        modelBuilder.Entity<UserShow>()
            .HasKey(us => new { us.UserId, us.ShowId });

        modelBuilder.Entity<UserShow>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserShows)
            .HasForeignKey(us => us.UserId);

        modelBuilder.Entity<UserShow>()
            .HasOne(us => us.Show)
            .WithMany(s => s.UserShows)
            .HasForeignKey(us => us.ShowId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.MediaItem)
            .WithMany(mi => mi.Reviews)
            .HasForeignKey(r => r.MediaItemId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
    }
}