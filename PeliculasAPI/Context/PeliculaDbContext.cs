using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Context
{
    public class PeliculaDbContext : DbContext
    {
        public DbSet<CategoriaEntidad> Categorias { get; set; }
        public DbSet<ActorEntidad> Actores { get; set; }
        public DbSet<PeliculaEntidad> Peliculas { get; set; }
        public DbSet<PeliculasActoresEntidad> PeliculasActores { get; set; }
        public DbSet<PeliculasCategoriasEntidad> PeliculasCategorias { get; set; }
        public DbSet<SalaDeCineEntidad> SalasDeCine { get; set; }
        public DbSet<PeliculasSalasDeCineEntidad> PeliculasSalasDeCines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActoresEntidad>().HasKey(model => new {model.ActorId, model.PeliculaId});

            modelBuilder.Entity<PeliculasCategoriasEntidad>().HasKey(model => new { model.CategoriaId, model.PeliculaId});

            modelBuilder.Entity<PeliculasSalasDeCineEntidad>().HasKey(x => new { x.PeliculaId, x.SalaDeCineId });

            base.OnModelCreating(modelBuilder);
        }
        public PeliculaDbContext(DbContextOptions options) : base(options) { }    
    }
}
