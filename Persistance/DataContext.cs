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
        public DbSet<UserTable> UserTables { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTable>()
                .HasData(
                    new UserTable {Id = 1, UserName = "Josh", Email ="josh@life.com" },
                    new UserTable {Id = 2, UserName = "Joshua", Email ="josasdfasdfh@life.com" },
                    new UserTable {Id = 3, UserName = "Joshie", Email ="joasdfsh@life.com" }
                );
        }
    }
}
