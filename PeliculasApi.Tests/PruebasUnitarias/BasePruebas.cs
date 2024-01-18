using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using PeliculasAPI.AutoMapper;
using PeliculasAPI.Context;

namespace PeliculasApi.Tests.PruebasUnitarias
{
    public class BasePruebas
    {
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

        protected void ConfigurarDependencias()
        {

        }
    }
}
