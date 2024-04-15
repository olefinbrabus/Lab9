using Microsoft.EntityFrameworkCore;

namespace Lab9.DB
{
    public class DbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>
            optionsBuilder.UseSqlite(@"Data Source=Animal.db");

    }
}
