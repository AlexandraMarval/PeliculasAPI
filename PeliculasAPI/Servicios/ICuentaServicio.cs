using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICuentaServicio
    {
        Task<RespuestasAutenticacionModelo> Registrar(CredencialesUsuario credencialesUsuario);
        Task<RespuestasAutenticacionModelo> Login(CredencialesUsuario credencialesUsuario);
    }
}
