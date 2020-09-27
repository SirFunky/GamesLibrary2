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
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        public DbSet<GamePublisher> GamePublishers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=GamesDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Game developer key relations           
            // Lägg till PK för GameDeveloper
            modelBuilder.Entity<GameDeveloper>()
                .HasKey(k => new { k.DeveloperId, k.GameId, k.StudioId});
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
            // Lägg till Relation mellan Studio och Gamedeveloper
            modelBuilder.Entity<GameDeveloper>()
                .HasOne(b => b.studio)
                .WithMany(bc => bc.GameDevelopers)
                .HasForeignKey(d => d.StudioId);
            #endregion
            #region Game publisher key relations
            // Lägg till PK för GamePublisher
            modelBuilder.Entity<GamePublisher>()
                .HasKey(k => new {k.GameId, k.PublisherId });
            // Lägg till Relation mellan Game och GamePublisher
            modelBuilder.Entity<GamePublisher>()
                .HasOne(b => b.Game)
                .WithMany(bc => bc.GamePublishers)
                .HasForeignKey(d => d.GameId);
            // Lägg till Relation mellan Publisher och GameDeveloper
            modelBuilder.Entity<GamePublisher>()
                .HasOne(b => b.publisher)
                .WithMany(bc => bc.GamePublishers)
                .HasForeignKey(d => d.PublisherId);

            #endregion
        }
    }

}
