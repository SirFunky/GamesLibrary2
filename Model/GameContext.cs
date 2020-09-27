using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesLibrary.Model
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=GamesDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Lägg till PK för GameDeveloper
            modelBuilder.Entity<GameDeveloper>()
                .HasKey(k => new { k.DeveloperId, k.GameId });
            // Lägg till Relation mellan Game och GameDeveloper
            modelBuilder.Entity<GameDeveloper>()
                .HasOne(b => b.Game)
                .WithMany(bc => bc.GameDevelopers)
                .HasForeignKey(d => d.GameId);
            // Lägg till Relation mellan Developer och GameDeveloper
            modelBuilder.Entity<GameDeveloper>()
               .HasOne(b => b.Developer)
               .WithMany(bc => bc.GameDevelopers)
               .HasForeignKey(d => d.DeveloperId);
        }
    }

}
