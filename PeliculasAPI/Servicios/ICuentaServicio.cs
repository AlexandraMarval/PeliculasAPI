using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICuentaServicio
    {
       public Task<TokenDeUsuario> Registrar(CredencialesUsuario credencialesUsuario);
       public Task<TokenDeUsuario> Login(CredencialesUsuario credencialesUsuario);
       public Task<List<UsuarioModelo>> ObtenerTodo(PaginacionModel paginacionModel);
       public Task<List<string>> ObtenerRoles();
       public Task<string> AsignarRol(EditarRolModelo editarRolModelo);
       public Task<string> RemoverRol(EditarRolModelo editarRolModelo);
    }
}
