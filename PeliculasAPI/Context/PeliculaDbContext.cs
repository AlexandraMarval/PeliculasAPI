using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Context
{
    public class PeliculaDbContext : DbContext
    {
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<ActorEntity> Actores { get; set; }
        public PeliculaDbContext(DbContextOptions options) : base(options) { }    
    }
}
