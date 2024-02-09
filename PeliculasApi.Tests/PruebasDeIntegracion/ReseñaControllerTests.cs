using Newtonsoft.Json;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using System.Net;
using Xunit;

namespace PeliculasApi.Tests.PruebasDeIntegracion
{
    public class ReseñaControllerTests : BasePruebas
    {
        // vamos a verificar que traiga 404 cuando la pelicula no exista

        private static readonly string templateUrl = "/api/pelicula/{0}/reseña";

        [Fact]
        public async Task DeberiaDevolver404CuandoLaPeliculaEsInexistente()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD);

            //Act Ejecutar
            var cliente = factory.CreateClient();
            var url = string.Format(templateUrl, 1);
            url += "?Pagina=12&CantidadRegistrosPorPagina=23'";
            var respuesta = await cliente.GetAsync(url);

            // Assert Verificar
            Assert.Equal(HttpStatusCode.NoContent, respuesta.StatusCode);
        }

        [Fact]
        public async Task DeberiaDevolverReseñasDeUnListadoVacio()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var factory = ConstruirWebApplicationFactory(nombreBD);
            var context = ConstruirContext(nombreBD);

            context.Peliculas.Add(new PeliculaEntidad() { Titulo = "pelicula 1" });
            await context.SaveChangesAsync();

            //Act Ejecutar
            var cliente = factory.CreateClient();
            var url = string.Format(templateUrl, 1);
            url += "?Pagina=1&CantidadRegistrosPorPagina=5";

            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            var respuestasDeLaAPI = JsonConvert.DeserializeObject<List<ReseñaModelo>>(await respuesta.Content.ReadAsStringAsync());

            // Assert Verificar
            Assert.Equal(0, respuestasDeLaAPI.Count);
        }
    }
}
