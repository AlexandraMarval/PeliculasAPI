using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/Cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;      
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ICuentaServicio servicio;
      
        public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICuentaServicio servicio)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.servicio = servicio;
        }

        [HttpPost("registrar", Name = "registrarUsuario")] // api/cuentas/registrar
        public async Task<ActionResult<RespuestasAutenticacionModelo>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = await servicio.Registrar(credencialesUsuario);
            return Ok(usuario);
        }

        [HttpPost("login", Name = "loginUsuario")]
        public async Task<ActionResult<RespuestasAutenticacionModelo>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await servicio.Login(credencialesUsuario); return Ok(resultado);
        }
    }
}
