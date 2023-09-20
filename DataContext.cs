global using Microsoft.EntityFrameworkCore;
using BooksApi.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BooksApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = minimalbookdb; Integrated Security = True; Persist Security Info = False; Pooling = False; Multiple Active Result Sets = False; Encrypt = False; Trust Server Certificate = False; Command Timeout = 0");
        }

        public DbSet<Book> Books => Set<Book>();
    }
}
