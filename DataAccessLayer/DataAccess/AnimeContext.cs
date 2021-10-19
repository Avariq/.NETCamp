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
    }
}
