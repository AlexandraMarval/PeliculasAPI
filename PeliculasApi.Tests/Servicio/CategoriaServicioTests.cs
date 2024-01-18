using FluentAssertions;
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
    public class CategoriaServicioTests : BasePruebas
    {
        // Purebas Automaticas
        [Fact]
        public async Task ObtenerListadoDeCategoria()
        {
            //arrange Preparar
            var nombreDB = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            context.Categorias.Add(new CategoriaEntidad() { Nombre = "Categoria 1" });
            context.Categorias.Add(new CategoriaEntidad() { Nombre = "Categoria 2" });

            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);
            var repositorio = new Repositorio<CategoriaEntidad>(context2);
            //Act Ejecutar
            var servicio = new CategoriaServicio(mapper, repositorio);
            var respuesta = await servicio.ObtenerCategorias();

            //Assert Verificar
            var categoria = respuesta;
            Assert.Equal(2, categoria.Count);
        }

        [Fact]
        public async Task ObtenerCategoriaPorIdNoExistente()
        {
            //arrange Preparar
            var nombreDB = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var context2 = ConstruirContext(nombreDB);
            var repositorio = new Repositorio<CategoriaEntidad>(context2);
            //Act Ejecutar

            var servicio = new CategoriaServicio(mapper, repositorio);
            var accion = () => servicio.ObtenerCategoriaPorId(2);
            //Assert Verificar
          await accion.Should().ThrowAsync<Exception>()
                .WithMessage("No hay un dato con ese id");
        }
    }
}