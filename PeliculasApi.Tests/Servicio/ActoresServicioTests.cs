using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using Moq;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;
using System.Text;
using Xunit;

namespace PeliculasApi.Tests.Servicio
{
    public class ActoresServicioTests : BasePruebas
    {
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private Mock<IAlmacenadorArchivos> mockAlmacenadorArchivos;

        public ActoresServicioTests()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockAlmacenadorArchivos = new Mock<IAlmacenadorArchivos>();
        }

        [Fact]
        public async Task ObtenerPersonasPaginadas()
        {
            //arrange Preparar
            
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            var actorEntity = context.Actores.Add(new ActorEntidad() { Nombre = "Actor 1" });
            var actorEntity1 = context.Actores.Add(new ActorEntidad() { Nombre = "Actor 2" });
            context.Actores.Add(new ActorEntidad() { Nombre = "Actor 3" });
            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreBD);
            var repositorio = new Repositorio<ActorEntidad>(context2);

                //Act Ejecutar
            var servicio = new ActoresServicio(mapper, repositorio, null, mockHttpContextAccessor.Object);

            var pagina = await servicio.ObtenerActores(new PaginacionModel() { Pagina = 1, CantidadRegistrosPorPagina = 2 });

            // assert  Verificar
            var registrosEsperadosEnPagina = new[] { 
                new { actorEntity.Entity.Id, actorEntity.Entity.Nombre }, 
                new { actorEntity1.Entity.Id, actorEntity1.Entity.Nombre } 
            };
            pagina.Should()
                .NotBeNull()
                .And.HaveCount(2, "porque se espera que la página contenga 2 elementos")
                .And.BeEquivalentTo(registrosEsperadosEnPagina); 
            mockHttpContext.Response.Headers.Should().ContainKey("cantidadPaginas");
            mockHttpContext.Response.Headers["cantidadPaginas"].Should().BeEquivalentTo(new[] { "2" });
        }

        [Fact]
        public async Task ObtenerPersonasPaginaDos()
        {
            //arrange Preparar

            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var mockHttpContext = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            context.Actores.Add(new ActorEntidad() { Nombre = "Actor 1" });
            context.Actores.Add(new ActorEntidad() { Nombre = "Actor 2" });
            var actorEntity = context.Actores.Add(new ActorEntidad() { Nombre = "El actor hermoso" });
            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreBD);
            var repositorio = new Repositorio<ActorEntidad>(context2);

            //Act Ejecutar
            var servicio = new ActoresServicio(mapper, repositorio, null, mockHttpContextAccessor.Object);

            var pagina = await servicio.ObtenerActores(new PaginacionModel() { Pagina = 2, CantidadRegistrosPorPagina = 2 });

            // assert  Verificar
            var registroEsperadoEnPagina = new[] { new { actorEntity.Entity.Id, actorEntity.Entity.Nombre } };
            pagina.Should()
                .NotBeNull()
                .And.HaveCount(1, "porque se espera que la página contenga 1 elementos")
                .And.BeEquivalentTo(registroEsperadoEnPagina);
            mockHttpContext.Response.Headers.Should().ContainKey("cantidadPaginas");
            mockHttpContext.Response.Headers["cantidadPaginas"].Should().BeEquivalentTo(new[] { "2" });
        }

        [Fact]
        public async Task CrearActorsinFoto()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var mockHttpContext = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            mockAlmacenadorArchivos.Setup(x => x.GuardarArchivo(null, null, null, null)).Returns(Task.FromResult("url"));

            var actor = new CrearActorModel() { Nombre = "Alexandra", FechaDeNacimiento = DateTime.Now };

            var repositorio = new Repositorio<ActorEntidad>(context);

            //Act Ejecutar
            var servicio = new ActoresServicio(mapper, repositorio, mockAlmacenadorArchivos.Object, mockHttpContextAccessor.Object);
              var respuesta = servicio.CrearActor(actor);

            // assert  Verificar
            var actorEsperado = new[] { new { actor.Nombre, actor.FechaDeNacimiento } };
            respuesta.Should()
                .NotBeNull()
                .And.BeEquivalentTo(respuesta);

            var context2 = ConstruirContext(nombreBD);
            var listado = await context2.Actores.ToListAsync();
            listado.Should().BeEquivalentTo(actorEsperado).And.NotBeNull (listado[0].Foto);
        }

        [Fact]
        public async Task CrearActorConFoto()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var mockHttpContext = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            var content = Encoding.UTF8.GetBytes("Imagen de prueba");
            var archivo = new FormFile (new MemoryStream(content), 0, content.Length, "Data", "Imagen.jpg");
            archivo.Headers = new HeaderDictionary();
            archivo.ContentType = "image/jpg";

            var actorEsperado = new CrearActorModel { Nombre = "nuevo actor", FechaDeNacimiento = DateTime.Now, Foto = archivo };

            mockAlmacenadorArchivos.Setup(x => x.GuardarArchivo(content, "jpg", "actores", archivo.ContentType)).Returns(Task.FromResult("url"));

            var repositorio = new Repositorio<ActorEntidad>(context);

            //Act Ejecutar
            var servicio = new ActoresServicio(mapper, repositorio,mockAlmacenadorArchivos.Object, mockHttpContextAccessor.Object);
            var resultado = await servicio.CrearActor(actorEsperado);

            // assert  Verificar
            resultado.Should().BeEquivalentTo(actorEsperado, options => options.Excluding(actor => actor.Foto));
        }

        [Fact]
        public async Task PathRetorna404SiActorNoExiste()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();
           

            var repositorio = new Repositorio<ActorEntidad>(context);

            //Act Ejecutar
            var servicio = new ActoresServicio(mapper, repositorio, mockAlmacenadorArchivos.Object, mockHttpContextAccessor.Object);
            var patchDoc = new JsonPatchDocument<ActorPatchModelo>();
            var  accion = () => servicio.ActualizarActorPatchId(1, patchDoc);

            // assert  Verificar
            accion.Should().NotBeNull();
            await accion.Should().ThrowAsync<Exception>()
               .WithMessage("No se encontró ninguna entidad con el id proporcionado");
        }

        [Fact]
        public async Task PatchActualizaUnSoloCampo()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var mockHttpContext = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            var fechaDeNacimiento = DateTime.Now;
            var actor = new ActorEntidad() { Nombre = "Alexandra", FechaDeNacimiento = fechaDeNacimiento };
            context.Add(actor);
            await context.SaveChangesAsync();

            var context2 = ConstruirContext(nombreBD);
            var repositorio = new Repositorio<ActorEntidad>(context2);
            //Act Ejecutar
            var servicio = new ActoresServicio(mapper, repositorio, mockAlmacenadorArchivos.Object, mockHttpContextAccessor.Object);

            var patchDoc = new JsonPatchDocument<ActorPatchModelo>();
            patchDoc.Operations.Add(new Operation<ActorPatchModelo>("replace", "/nombre", null, "Henksando"));

            var resultadoEsperado = await servicio.ActualizarActorPatchId(1,patchDoc);

            var context3 = ConstruirContext(nombreBD);
            var actorDB = await context3.Actores.FirstAsync();

            // assert  Verificar
           Equals("Henksando", actorDB.Nombre);
           Equals(fechaDeNacimiento, actorDB.FechaDeNacimiento);

        }

        private ActoresServicio ConstruirServicio(string nombreBD)
        {
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var repositorio = new Repositorio<ActorEntidad>(context);

            var mockHttpContext = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            mockAlmacenadorArchivos.Setup(x => x.GuardarArchivo(null, null, null, null)).Returns(Task.FromResult("url"));

            return new ActoresServicio(mapper, repositorio, mockAlmacenadorArchivos.Object, mockHttpContextAccessor.Object);
        }
    }
}
