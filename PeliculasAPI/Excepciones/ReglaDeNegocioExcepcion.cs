namespace PeliculasAPI.Excepciones
{
    public class ReglaDeNegocioExcepcion : Exception
    {
        public ReglaDeNegocioExcepcion(string mensaje) : base(mensaje)
        {
        }
    }
}
