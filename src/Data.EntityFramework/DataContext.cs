using Microsoft.EntityFrameworkCore;
using Model;

namespace Data.EntityFramework
{
    public class DataContext : DbContext
    {
        public string Connstring = "Data Source=localhost; Initial Catalog=POCDb; Integrated Security=SSPI;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connstring);
        }

        public DbSet<Messages> Messages { get; set; }
    }
}