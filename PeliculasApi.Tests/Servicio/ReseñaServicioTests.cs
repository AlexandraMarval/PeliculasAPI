using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
    public class ReseñaServicioTests : BasePruebas
    {
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;

        public ReseñaServicioTests()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        [Fact]
        public async Task UsuarioNoPuedeCrearDosReseñasParaLaMismaPelicula()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var httpContextAccessor = ConfigurarServicioContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContextAccessor.HttpContext);

            CrearPeliculas(nombreBD);

            var peliculaId = context.Peliculas.Select(x => x.Id).First();

            var reseñaExistente = new ReseñaEntidad() { PeliculaId = peliculaId, UsuarioId = usurioPorDefectoId, Puntuacion = 5};

            context.Add(reseñaExistente);
            await context.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var repositorio = new ReseñaRepositorio(contexto2);

            var contexto = ConstruirContext(nombreBD);
            var repositorio1 = new PeliculaRepositorio(contexto);

            var servicio = new ReseñaServicio(repositorio, mapper,repositorio1, mockHttpContextAccessor.Object);

            var crearReseñaModelo = new CrearReseñaModelo { Puntuacion = 5 };

            //Act Ejecutar
            var delegado = () => servicio.CrearUnaReseña(peliculaId, crearReseñaModelo);

            //Assert Verificar
            await delegado.Should().ThrowAsync<Exception>("El usuario ya ha escrito una reseña de esta pelicula");
        }

        [Fact]
        public async Task CrearReseña()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var httpContextAccessor = ConfigurarServicioContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContextAccessor.HttpContext);

            CrearPeliculas(nombreBD);

            var peliculaId = context.Peliculas.Select(x => x.Id).First();

            var contexto2 = ConstruirContext(nombreBD);
            var repositorio = new ReseñaRepositorio(contexto2);

            var contexto = ConstruirContext(nombreBD);
            var repositorio1 = new PeliculaRepositorio(contexto);

            var servicio = new ReseñaServicio(repositorio, mapper,repositorio1, mockHttpContextAccessor.Object);

            var crearReseñaModelo = new CrearReseñaModelo { Puntuacion = 5 };

            //Act Ejecutar
            var resultado = await servicio.CrearUnaReseña(peliculaId, crearReseñaModelo);
            //Assert Verificar

            resultado.Should().NotBeNull();
            var contexto3 = ConstruirContext(nombreBD);
            var reseñaDBExperada = contexto3.Reseñas.First();

            resultado.Should().BeEquivalentTo(reseñaDBExperada, 
                options => options.ExcludingMissingMembers());
        }

        private void CrearPeliculas(string nombreDB)
        {
            var contexto = ConstruirContext(nombreDB);
            contexto.Peliculas.Add(new PeliculaEntidad() { Titulo = "pelicula 1" });
            contexto.SaveChangesAsync();
        }
    }
}
