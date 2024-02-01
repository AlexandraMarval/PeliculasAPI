using FluentAssertions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Entidades;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        //    using (var context = LocalDbDatabaseInitializer.GetDbContextLocalDb(false))
        //    {
        //        var salasDeCine = new List<SalaDeCineEntidad>()
        //        {
        //                        new SalaDeCineEntidad { Id = 3, Nombre = "Palafox", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-0.88257091473750082, 41.650913765831994)) }
        //        };

        //        context.AddRange(salasDeCine);
        //        await context.SaveChangesAsync();
        //    }
           
        //}
    }
}
