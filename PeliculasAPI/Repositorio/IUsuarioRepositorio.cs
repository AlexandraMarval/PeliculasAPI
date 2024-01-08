using Microsoft.AspNetCore.Identity;
using PeliculasAPI.Modelos;
using System.Linq.Expressions;

namespace PeliculasAPI.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<IdentityUser>
    {
        Task<List<string>> ObtenerRoles();
    }
}
