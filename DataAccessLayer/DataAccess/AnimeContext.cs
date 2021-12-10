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
        }
    }
}
