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
        }
    }
}
