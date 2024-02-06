using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
