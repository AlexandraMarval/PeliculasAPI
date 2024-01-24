using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
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
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            context.Categorias.Add(new CategoriaEntidad() { Nombre = "Categoria 1" });
            context.Categorias.Add(new CategoriaEntidad() { Nombre = "Categoria 2" });

            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreBD);
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
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var context2 = ConstruirContext(nombreBD);
            var repositorio = new Repositorio<CategoriaEntidad>(context2);
            //Act Ejecutar

            var servicio = new CategoriaServicio(mapper, repositorio);
            var accion = () => servicio.ObtenerCategoriaPorId(2);
            //Assert Verificar
          await accion.Should().ThrowAsync<Exception>()
                .WithMessage("No hay un dato con ese id");
        }

        [Fact]
        public async Task ObtenerCategoriaPorIdExistente()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            CategoriaEntidad categoriaEsperada = new () { Nombre = " Categoria Que voy a buscar" };

            context.Categorias.Add(new CategoriaEntidad() { Nombre = " Categoria 2" });
            context.Categorias.Add(categoriaEsperada);
            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreBD);
            var repositorio = new Repositorio<CategoriaEntidad>(context2);
            //Act Ejecutar

            var servicio = new CategoriaServicio(mapper, repositorio);

            var id = 2;
            var respuesta = await servicio.ObtenerCategoriaPorId(id);

            // assert  Verificar
            respuesta.Should().NotBeNull();
            respuesta.Should().BeEquivalentTo(new { categoriaEsperada.Id, categoriaEsperada.Nombre });
        }

        [Fact]
        public async Task CrearCategoria()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

           var categoriaEsperada = new  CrearCategoriaModelo() { Nombre = "nueva Categoria" };

            var repositorio = new Repositorio<CategoriaEntidad>(context);

            //Act Ejecutar

            var servicio = new CategoriaServicio(mapper, repositorio);
            var respuesta = await servicio.CrearCategoria(categoriaEsperada);

            // assert  Verificar
            respuesta.Should().BeEquivalentTo(new { categoriaEsperada.Nombre});
        }

        [Fact]
        public async Task ActualizarCategoria()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            context.Categorias.Add(new CategoriaEntidad() { Nombre = "Categoria 1" });
            await context.SaveChangesAsync();
            var context2 = ConstruirContext(nombreBD);

            var repositorio = new Repositorio<CategoriaEntidad>(context2);

            //Act Ejecutar
            var servicio = new CategoriaServicio(mapper, repositorio);

            var actualizarCategoriaModelo = new ActualizarCategoriaModelo() { Nombre = "Nuevo nombre"};

            var id = 1;
            var respuesta = servicio.ActualizarCategoria(id, actualizarCategoriaModelo);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.Categorias.AnyAsync( x => x.Nombre == "Nuevo nombre");
          
            // assert  Verificar

            Assert.True(existe);
        }

        [Fact]
        public async Task EliminarCategoriaNoExistente()
        {

            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var repositorio = new Repositorio<CategoriaEntidad>(context);

            //Act Ejecutar
            var servicio = new CategoriaServicio(mapper, repositorio);
            var accion = () => servicio.EliminarCategoria(1);
            //Assert Verificar
            await accion.Should().ThrowAsync<Exception>()
                  .WithMessage("No existe un actor por el mismo id");
        }

        [Fact]
        public async Task EliminarCategoria()
        {
            //arrange Preparar
            var nombreDB = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            context.Categorias.Add(new CategoriaEntidad() { Nombre = "Categoria 1" });
            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);

            var repositorio = new Repositorio<CategoriaEntidad>(context2);

            //Act Ejecutar
            var servicio = new CategoriaServicio(mapper, repositorio);
            var respuesta = servicio.EliminarCategoria(1);

            var contexto3 = ConstruirContext(nombreDB);
            var existe = await contexto3.Categorias.AnyAsync();
            //Assert Verificar
           Assert.False(existe);
        }
    }
}