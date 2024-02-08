using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using Xunit;

namespace PeliculasApi.Tests.PruebasDeIntegracion
{
    // Prueba que involucra todo el engranage de nuestro proyecto Asp.net.core
    public class CategoriaControllerTests : BasePruebas
    {
        private static readonly string url = "/api/categoria";

        [Fact]
        public async Task ObtenerTodasLasCategoriasDeListadoVacio()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD);

            //Act Ejecutar
            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);

            //Assert Verificar
            respuesta.EnsureSuccessStatusCode();
            var respuestasDeLaAPI = JsonConvert.DeserializeObject<List<CategoriaModelo>>(await respuesta.Content.ReadAsStringAsync());
            respuestasDeLaAPI.Should().HaveCount(0);
        }


        [Fact]  
        public async Task ObtenerTodasLasCategorias()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD);
            var context = ConstruirContext(nombreBD);
            context.Categorias.Add(new CategoriaEntidad() { Nombre = "categoria 1"});
            context.Categorias.Add(new CategoriaEntidad() { Nombre = "categoria 2"});
            await context.SaveChangesAsync();

            //Act Ejecutar
            var cliente = factory.CreateClient();
            var respuesta = await cliente.GetAsync(url);

            //Assert Verificar
            respuesta.EnsureSuccessStatusCode();
            var respuestasDeLaAPI = JsonConvert.DeserializeObject<List<CategoriaModelo>>(await respuesta.Content.ReadAsStringAsync());
            respuestasDeLaAPI.Should().HaveCount(2);
        }

        [Fact]
        public async Task BorrarCategorias()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD);
            var context = ConstruirContext(nombreBD);

            context.Categorias.Add(new CategoriaEntidad() { Nombre = "categoria 1" });
            await context.SaveChangesAsync();

            //Act Ejecutar
            var cliente = factory.CreateClient();
            var respuesta = await cliente.DeleteAsync($"{url}/1");

            respuesta.EnsureSuccessStatusCode();

            var context2 = ConstruirContext(nombreBD);
            var existe = await context2.Categorias.AnyAsync();

            //Assert Verificar
            Assert.False(existe);
        }

        [Fact]
        public async Task BorrarCategoriasDebeRetorna401()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD, false);

            //Act Ejecutar
            var cliente = factory.CreateClient();
            var respuesta = await cliente.DeleteAsync($"{url}/1");
            //Assert Verificar
            Assert.Equal("Unauthorized", respuesta.ReasonPhrase);
        }
    }
}
