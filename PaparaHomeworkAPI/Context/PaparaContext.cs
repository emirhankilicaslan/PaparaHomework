using Microsoft.EntityFrameworkCore;
using PaparaHomeworkAPI.Entities;

namespace PaparaHomeworkAPI.Context
{
    public class PaparaContext : DbContext
    {
        public PaparaContext(DbContextOptions<PaparaContext> options) : base(options) 
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}