
using FluentAssertions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;
using Xunit;

namespace PeliculasApi.Tests.Servicio
{
    public class SalaDeCineServicioTests : BasePruebas
    {
        [Fact]
        public async Task ObtenerListadoDeSalaDeCine()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            context.SalasDeCine.Add(new SalaDeCineEntidad() { Nombre = "Aragonia 1"});
            context.SalasDeCine.Add( new SalaDeCineEntidad() { Nombre = "Aragonia 2" });
            context.SalasDeCine.Add(new SalaDeCineEntidad() { Nombre = "Aragonia 3" });
            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreBD);
            var repositorio = new SalaDeCineRepositorio(context2);
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            await context.SaveChangesAsync();
            //Act Ejecutar
            var servicio = new SalaDeCineServicio(mapper, repositorio, geometryFactory);
            var resultado = await servicio.ObtenerSalaDeCine();

            //Assert Verificar
            var salaDeCine = resultado;
            Assert.Equal(3, salaDeCine.Count);
        }

        //[Fact]
        //public async Task ObtenerSalaDeCineAskilometrosOMenos()
        //{

        //    //arrange Preparar
        //    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        //    using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
        //    {
        //        var salasDeCine = new List<SalaDeCineEntidad>()
        //        {
        //             new SalaDeCineEntidad { Nombre = "Palafox", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-0.88257091473750082, 41.650913765831994)) }
        //        };

        //        context.AddRange(salasDeCine);
        //        await context.SaveChangesAsync();

        //        var filtro = new SalaDeCineCercanoFiltroModelo()
        //        {
        //            DistanciaEnKms = 5,
        //            Longitud = 41.650913765831994,
        //            Latitud = -0.88257091473750082
        //        };
                
        //        //Act Ejecutar
        //        using (var contexto = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
        //        {
        //            var mapper = ConfigurarAutoMapper();

        //            var repositorio = new SalaDeCineRepositorio(contexto);

        //            var servicio = new SalaDeCineServicio(mapper, repositorio, geometryFactory);
        //            var resultado = await servicio.Cercano(filtro);

        //            //Assert Verificar
        //            resultado.Count.Should().Be(2);
        //        }

        //    }
        //}
    }
}
