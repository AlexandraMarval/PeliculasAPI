using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeliculasAPI.Servicios
{  
    public class CuentaServicio : ICuentaServicio
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;        
        private readonly IUsuarioRepositorio repositorio;
        private readonly IMapper mapper;

        public CuentaServicio(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUsuarioRepositorio repositorio, IMapper mapper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;          
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        public async Task<TokenDeUsuario> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.Email, Email = credencialesUsuario.Email };
            var resultado = await userManager.CreateAsync(usuario,credencialesUsuario.Password);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                throw new Exception($"{resultado.Errors}");
            }
        }

        public async Task<TokenDeUsuario> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email, credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                throw new Exception("Login Incorrecto");
            }
        }

        private async Task<TokenDeUsuario> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  credencialesUsuario.Email),
                new Claim(ClaimTypes.Email, credencialesUsuario.Email)              
            };

            var identidadDelUsuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, identidadDelUsuario.Id));

            var claimsDB = await userManager.GetClaimsAsync(identidadDelUsuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: credenciales);

            return new TokenDeUsuario()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

        public async Task<List<UsuarioModelo>> ObtenerTodo(PaginacionModel paginacionModel)
        {
            var usuarios = await repositorio.ObtenerTodoPaginado(x => true, x => x.Email, paginacionModel);

            return mapper.Map<List<UsuarioModelo>>(usuarios);
        }

        public async Task<List<string>> ObtenerRoles()
        {
            var usuario = await repositorio.ObtenerRoles();
            return usuario.ToList();
        }

        public async Task<string> AsignarRol(EditarRolModelo editarRolModelo)
        {
            var user = await userManager.FindByIdAsync(editarRolModelo.UsuarioId);

            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editarRolModelo.NombreRol));
            throw new Exception();
        }

        public async Task<string> RemoverRol(EditarRolModelo editarRolModelo)
        {
            var user = await userManager.FindByIdAsync(editarRolModelo.UsuarioId);
            await userManager.RemoveClaimAsync(user,new Claim(ClaimTypes.Role, editarRolModelo.NombreRol));
            throw new Exception();
        }
    }
}
