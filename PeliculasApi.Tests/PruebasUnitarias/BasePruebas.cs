using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NetTopologySuite;
using PeliculasAPI.AutoMapper;
using PeliculasAPI.Context;
using System.Security.Claims;

namespace PeliculasApi.Tests.PruebasUnitarias
{
    public class BasePruebas
    {
        protected string usurioPorDefectoId = "c1b262eb-0803-431b-bf48-8e9fa979642";
        protected string usurioPorDefectoEmail = "ejemplo@hotmail.com";

        protected PeliculaDbContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<PeliculaDbContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new PeliculaDbContext(opciones);
            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var configurar = new MapperConfiguration(options =>
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                options.AddProfile(new AutoMapperProfiles(geometryFactory));
            });
            return configurar.CreateMapper();
        }

        protected ControllerContext ConfigurarServicioContext()
        {
            var usuario = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usurioPorDefectoEmail),
                new Claim(ClaimTypes.Email, usurioPorDefectoEmail),
                new Claim(ClaimTypes.NameIdentifier, usurioPorDefectoId),
            }));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = usuario },
            };
        }

        protected WebApplicationFactory<Program> ConstruirWebApplicationFactory(string nombreDB, bool ignorarSeguridad = true)
        {
            var factory = new WebApplicationFactory<Program>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(servicios =>
                {
                    var descriptorDBContext = servicios.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PeliculaDbContext>));

                    if (descriptorDBContext != null)
                    {
                        servicios.Remove(descriptorDBContext);
                    }

                    servicios.AddDbContext<PeliculaDbContext>(options => options.UseInMemoryDatabase(nombreDB));

                    if (ignorarSeguridad)
                    {
                        servicios.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();

                        servicios.AddControllers(options=> 
                        {
                            options.Filters.Add(new UsuarioFalsoFiltro());
                        });
                    }
                });
            });
            return factory;
        }
    }
}
