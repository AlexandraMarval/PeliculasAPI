using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICuentaServicio
    {
        Task<RespuestasAutenticacionModelo> Registrar(CredencialesUsuario credencialesUsuario);
        Task<RespuestasAutenticacionModelo> Login(CredencialesUsuario credencialesUsuario);
        Task<List<UsuarioModelo>> ObtenerTodo(PaginacionModel paginacionModel);
        Task<List<string>> ObtenerRoles();
        Task<string> AsignarRol(EditarRolModelo editarRolModelo);
        Task<string> RemoverRol(EditarRolModelo editarRolModelo);
    }
}
