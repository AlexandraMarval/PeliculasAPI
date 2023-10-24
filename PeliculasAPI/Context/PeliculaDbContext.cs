using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Context
{
    public class PeliculaDbContext : DbContext
    {
        public PeliculaDbContext(DbContextOptions options) : base(options) { }    
    }
}
