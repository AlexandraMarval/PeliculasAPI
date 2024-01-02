using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICuentaServicio
    {
        Task<RespuestasAutenticacionModelo> Registrar(CredencialesUsuario credencialesUsuario);
    }
}
