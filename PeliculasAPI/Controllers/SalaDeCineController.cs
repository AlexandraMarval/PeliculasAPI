using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/salasDeCine")]
    public class SalaDeCineController : ControllerBase
    {
        private readonly ISalaDeCineServicio servicio;

        public SalaDeCineController(ISalaDeCineServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineModelo>>> ObtenerListadoDeSalaDeCine()
        {
            var salaDeCine = await servicio.ObtenerSalaDeCine();
            if (salaDeCine == null)
            {
                NotFound("No se encontro resultado de su busqueda");
            }
            return Ok(salaDeCine);
        }
    }
}
