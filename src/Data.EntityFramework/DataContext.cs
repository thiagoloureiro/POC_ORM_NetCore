using Data.Base;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.EntityFramework
{
    public class DataContext : DbContext
    {
        public string Connstring => new BaseRepository().ConnstringDbPoc;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connstring);
        }

        public DbSet<Person> Person { get; set; }
    }
}