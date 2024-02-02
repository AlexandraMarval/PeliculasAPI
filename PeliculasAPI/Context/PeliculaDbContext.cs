using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.Entidades;
using System.Security.Claims;

namespace PeliculasAPI.Context
{
    public class PeliculaDbContext : IdentityDbContext
    {

        public PeliculaDbContext(DbContextOptions options) : base(options) { }
        public DbSet<CategoriaEntidad> Categorias { get; set; }
        public DbSet<ActorEntidad> Actores { get; set; }
        public DbSet<PeliculaEntidad> Peliculas { get; set; }
        public DbSet<PeliculasActoresEntidad> PeliculasActores { get; set; }
        public DbSet<PeliculasCategoriasEntidad> PeliculasCategorias { get; set; }
        public DbSet<SalaDeCineEntidad> SalasDeCine { get; set; }
        public DbSet<PeliculasSalasDeCineEntidad> PeliculasSalasDeCines { get; set; }
        public DbSet<ReseñaEntidad> Reseñas { get; set; }

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

            var usuarioNombre = "AlexandraM@Hotmail.com";

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

            modelBuilder.Entity<SalaDeCineEntidad>()
              .HasData(new List<SalaDeCineEntidad>
              {
                    new SalaDeCineEntidad{Id = 3, Nombre = "palafox", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-0.88257091473750082, 41.650913765831994))},

                    new SalaDeCineEntidad{Id = 4, Nombre = "Cines Unidos", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-66.917333,  10.504223))},

                    new SalaDeCineEntidad{Id = 5, Nombre = "Cinex", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-66.8773,  10.504223))},

                    new SalaDeCineEntidad{Id = 6, Nombre = "Cines Aragonia", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-0.908871,  41.639305))}
              });

            var acción = new CategoriaEntidad() { Id = 1, Nombre = "Acción" };
            var romance = new CategoriaEntidad() { Id = 2, Nombre = "Romance" };
            var terror = new CategoriaEntidad() { Id = 3, Nombre = "Terror" };
            var infantil = new CategoriaEntidad() { Id = 4, Nombre = "Infantil" };

             modelBuilder.Entity<CategoriaEntidad>()
                .HasData(new List<CategoriaEntidad>
                {
                    acción, romance, terror, infantil
                });

            var samuelJackson = new ActorEntidad { Id = 1, Nombre = "Samuel L. Jackson", FechaDeNacimiento = new DateTime(1948, 12, 21) };
            var tomHolland = new ActorEntidad() { Id = 2, Nombre = "Tom Holland", FechaDeNacimiento = new DateTime(1996, 6, 1) };
            var tobeyMaguire = new ActorEntidad() { Id = 7, Nombre = "Tobey Maguire", FechaDeNacimiento = new DateTime(1975, 06, 1) };

            modelBuilder.Entity<ActorEntidad>()
               .HasData(new List<ActorEntidad>
               {
                    samuelJackson, tomHolland, tobeyMaguire
               });

            var endgame = new PeliculaEntidad()
            {
                Id = 1,
                Titulo = "SPIDER-MAN: NO WAY HOME",
                EnCine = true,
                FechaEstreno = new DateTime(2019, 06, 28)
            };

            modelBuilder.Entity<PeliculaEntidad>()
               .HasData(new List<PeliculaEntidad>
               {
                    endgame
               });
        }   
    }
}
