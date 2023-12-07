using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Ayudantes
{
    public static class HttpContextExtensiones
    {
        public static async Task InsertarParametrosPaginacion<T>(this HttpContext httpcontext, IQueryable<T> values, int cantidadRegistrosPorPagina)
        {
            if (httpcontext == null) { throw new ArgumentNullException(nameof(httpcontext)); }

            double cantidad = await values.CountAsync();
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistrosPorPagina);
            httpcontext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }
    }
}
