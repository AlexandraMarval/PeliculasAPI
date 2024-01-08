using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Ayudantes;
using PeliculasAPI.Context;
using PeliculasAPI.Modelos;
using System.Linq.Expressions;

namespace PeliculasAPI.Repositorio
{
    public class UsuarioRepositorio : Repositorio<IdentityUser>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(PeliculaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<string>> ObtenerRoles()
        {
            return await dbContext.Roles.Select(x => x.Name).ToListAsync();
        }
    }
}