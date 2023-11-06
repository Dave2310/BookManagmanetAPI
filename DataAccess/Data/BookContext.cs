using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataAccess.Data
{
    public class BookContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public BookContext(IConfiguration configuration, DbContextOptions<BookContext> options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=BookManagment;Trusted_connection=true;Encrypt=false");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region DbSet
        public DbSet<Book> Book { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Review> Review { get; set; } = null!;
        #endregion
    }
}

