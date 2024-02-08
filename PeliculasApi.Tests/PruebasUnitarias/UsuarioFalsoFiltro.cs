using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasApi.Tests.PruebasUnitarias
{
    public class UsuarioFalsoFiltro : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Email, "example@hotmail.com"),
                new Claim(ClaimTypes.Name, "example@hotmail.com"),
                new Claim(ClaimTypes.NameIdentifier, "c5r262eb-0803-597v-bf48-8e9fa972379"),
            }));

          await next();  
        }
    }
}
