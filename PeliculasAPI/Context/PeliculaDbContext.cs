using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Context
{
    public class PeliculaDbContext : DbContext
    {
        public DbSet<CategoriaEntity> Categorias { get; set; }
        public DbSet<ActorEntity> Actores { get; set; }
        public DbSet<PeliculaEntidad> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasCategorias> PeliculasCategorias { get; set; }
        public DbSet<SalaDeCine> SalasDeCine { get; set; }
        public DbSet<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>().HasKey(model => new {model.ActorId, model.PeliculaId});

            modelBuilder.Entity<PeliculasCategorias>().HasKey(model => new { model.CategoriaId, model.PeliculaId});

            modelBuilder.Entity<PeliculasSalasDeCine>().HasKey(x => new { x.PeliculaId, x.SalaDeCineId });

            base.OnModelCreating(modelBuilder);
        }
        public PeliculaDbContext(DbContextOptions options) : base(options) { }    
    }
}
