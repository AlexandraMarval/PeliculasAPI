using Microsoft.AspNetCore.Authorization;

namespace PeliculasApi.Tests.PruebasUnitarias
{
    public class AllowAnonymousHandler : IAuthorizationHandler
    {
        // Para saltarse las medidas de seguridad
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach(var requirement in context.Requirements.ToList())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
