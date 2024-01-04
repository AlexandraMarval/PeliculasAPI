using Microsoft.AspNetCore.Identity;
using PeliculasAPI.Modelos;
using System.Linq.Expressions;

namespace PeliculasAPI.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<IdentityUser>
    {
        Task<List<IdentityUser>> ObtenerTodo(Expression<Func<IdentityUser, string>> expressionOrder, PaginacionModel paginacionModel);
        Task<List<string>> ObtenerRoles();
    }
}
