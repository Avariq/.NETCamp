using AnimeLib.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeLib.Domain.DataAccess
{
    public class AnimeContext : DbContext
    {
        public AnimeContext(DbContextOptions options) : base(options) { }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Arc> Arcs { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<AgeRestriction> AgeRestrictions { get; set; }
        public DbSet<AnimeGenre> AnimeGenre { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeGenre>()
                .HasKey(k => new { k.GenreId, k.AnimeId });
            modelBuilder.Entity<Anime>()
                .Property(a => a.Rating)
                .HasDefaultValue(0);
            modelBuilder.Entity<Anime>()
                .Property(a => a.Views)
                .HasDefaultValue(0);
            modelBuilder.Entity<Anime>()
                .HasIndex(a => a.Title)
                .IsUnique();
            modelBuilder.Entity<Status>()
                .HasIndex(s => s.StatusName)
                .IsUnique();
            modelBuilder.Entity<AgeRestriction>()
                .HasIndex(ar => ar.RestrictionCode)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasData(new User { 
                    Id = 1,
                    Username = "balvaron", 
                    PasswordHash = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f", 
                    Email = "some.email@gmail.com", 
                    RoleId = 1 });
            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole
                {
                    Id = 1,
                    Role = "Admin"
                });
            modelBuilder.Entity<AgeRestriction>()
                .HasData(
                    new AgeRestriction { Id = 1, RestrictionCode = "G", AgeRequired = 0 },
                    new AgeRestriction { Id = 2, RestrictionCode = "PG12", AgeRequired = 12 },
                    new AgeRestriction { Id = 3, RestrictionCode = "R15", AgeRequired = 15 },
                    new AgeRestriction { Id = 4, RestrictionCode = "R18", AgeRequired = 18 });
            modelBuilder.Entity<Status>()
                .HasData(
                    new Status { Id = 1, StatusName = "Ongoing" },
                    new Status { Id = 2, StatusName = "Released" },
                    new Status { Id = 3, StatusName = "Announced" }
                );
            modelBuilder.Entity<Genre>()
                .HasData(
                    new Genre { Id = 1, Name = "Seinen" }
                );
            modelBuilder.Entity<Anime>()
                .HasData(
                    new Anime
                    {
                        Id = 1,
                        Title = "Bleach",
                        Description = "Bleach is a Japanese manga series written and illustrated by Tite Kubo. It follows the adventures of the hotheaded teenager Ichigo Kurosaki, who inherits his parents' destiny after he obtains the powers of a Soul Reaper—a death personification similar to the Grim Reaper—from another Soul Reaper, Rukia Kuchiki. His new-found powers force him to take on the duties of defending humans from evil spirits and guiding departed souls to the afterlife, and set him on journeys to various ghostly realms of existence.",
                        Image = "Images/bleach.jpg",
                        StatusId = 2,
                        AgeRestrictionId = 3,
                        Year = 2001
                    }
                );
            modelBuilder.Entity<AnimeGenre>()
                .HasData(
                    new AnimeGenre { AnimeId = 1, GenreId = 1 }
                );

        }
    }
}
