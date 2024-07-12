using Microsoft.EntityFrameworkCore;
using Valhalla_Music.Entities;

namespace Valhalla_Music
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set;}

        






    }
}