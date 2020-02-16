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

        // public DbSet<UserGame> UserGame {get; set; }
        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User {Id = 1, UserName = "Josh Wren", Email ="joshisplutar@gmail.com", },
                    new User {Id = 2, UserName = "Tim", Email ="josasdfasdfh@life.com"},
                    new User {Id = 3, UserName = "Bob", Email ="joasdfsh@life.com"}
                );
            modelBuilder.Entity<Game>()
                .HasData(
                    new Game {GameId = 1, GameName ="Goblin Battle", UserId = 1},
                    new Game {GameId = 2, GameName ="Orc Battle", UserId = 1},
                    new Game {GameId = 3, GameName ="Dragon Battle", UserId = 1},
                    new Game {GameId = 4, GameName ="Minotaur Battle", UserId = 2}

                );
        }
    }
}
