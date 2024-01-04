using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using PeliculasAPI.Entidades;
using System.Security.Claims;

namespace PeliculasAPI.Context
{
    public class PeliculaDbContext : IdentityDbContext
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

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var rolAdminId = "654e70d5-ca99-4ca9-b081-01298bb91c75";
            var usuarioAdminId = "dbcfecf9-f05a-49b7-bae0-38a564c70949";

            var rolAdmin = new IdentityRole()
            {
                Id = rolAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var contraseñaHasher = new PasswordHasher<IdentityUser>();

            var usuarioNombre = "ALEXANDRAM@HOTMAIL.COM";

            var usuarioAdmin = new IdentityUser()
            {
                Id = usuarioAdminId,
                UserName = usuarioNombre,
                NormalizedUserName = usuarioNombre,
                Email = usuarioNombre.ToLower(),
                PasswordHash = contraseñaHasher.HashPassword(null,"Am1234$")
            };

            //modelBuilder.Entity<IdentityUser>()
            //    .HasData(usuarioAdmin);

            //modelBuilder.Entity<IdentityRole>()
            //    .HasData(rolAdmin);

            //modelBuilder.Entity<IdentityUserClaim<string>>()
            //    .HasData(new IdentityUserClaim<string>()
            //    {
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        UserId = usuarioAdminId,
            //        ClaimValue = "Admin",

            //    });

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }
        public PeliculaDbContext(DbContextOptions options) : base(options) { }    
    }
}
