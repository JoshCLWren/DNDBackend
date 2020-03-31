using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class DataContext: DbContext 
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Game {get; set; }

        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(

                    new User {Id = 10220438528782809, UserName = "Josh Wren"},
                    new User {Id = 20220438528782809, UserName = "Tim"},
                    new User {Id = 30220438528782809, UserName = "Bob"}
                );
            modelBuilder.Entity<Game>()
                .HasData(
                    new Game {GameId  = Guid.NewGuid(), GameName ="Goblin Battle", UserId = 10220438528782809},
                    new Game {GameId  = Guid.NewGuid(), GameName ="Orc Battle", UserId = 10220438528782809},
                    new Game {GameId  = Guid.NewGuid(), GameName ="Dragon Battle", UserId = 20220438528782809},
                    new Game {GameId  = Guid.NewGuid(), GameName ="Minotaur Battle", UserId = 30220438528782809}

                );
        }
    }
}
