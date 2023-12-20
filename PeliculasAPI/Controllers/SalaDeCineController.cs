﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet(Name = "ObtenerListadoSalaDeCine")]
        public async Task<ActionResult<List<SalaDeCineModelo>>> ObtenerListadoDeSalaDeCine()
        {
            var salaDeCine = await servicio.ObtenerSalaDeCine();
            if (salaDeCine == null)
            {
                NotFound("No se encontro resultado de su busqueda");
            }
            return Ok(salaDeCine);
        }

        [HttpGet("{id}", Name = "ObtenerSalaDeCinePorId")]
        public async Task<ActionResult<SalaDeCineModelo>> ObtenerPorId(int id)
        {
            var salaDeCine = await servicio.ObtenerPorId(id);
            if (salaDeCine == null)
            {
                NotFound("No se encontro el id");
            }
            return Ok(salaDeCine);
        }

        [HttpPost(Name = "CrearSalaDeCine")]
        public async Task<ActionResult> CrearSalaDeCine(CrearSalaDeCineModelo crearSalaDeCineModelo)
        {
            var salaDeCine = await servicio.CrearSalaDeCine(crearSalaDeCineModelo);
            if (salaDeCine == null)
            {
                return NotFound("No se encontro resultado");
            }
            return new CreatedAtRouteResult("ObtenerSalaDeCinePorId", new { id = salaDeCine.Id}, salaDeCine);
        }
    }
}
