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
                    // everything broke when I changed my model to long numbers to accommadte the facebook id
                    new User {Id = 10220438528782809, UserName = "Josh Wren", Email ="joshisplutar@gmail.com", },
                    new User {Id = 20220438528782809, UserName = "Tim", Email ="josasdfasdfh@life.com"},
                    new User {Id = 30220438528782809, UserName = "Bob", Email ="joasdfsh@life.com"}
                );
            modelBuilder.Entity<Game>()
                .HasData(
                    new Game {GameId = 40220438528782809, GameName ="Goblin Battle", UserId = 1},
                    new Game {GameId = 50220438528782809, GameName ="Orc Battle", UserId = 1},
                    new Game {GameId = 60220438528782809, GameName ="Dragon Battle", UserId = 1},
                    new Game {GameId = 70220438528782809, GameName ="Minotaur Battle", UserId = 2}

                );
        }
    }
}
