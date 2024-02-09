using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace PeliculasAPI.Ayudantes
{
    public class FiltroErrores : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroErrores> logger;

        public FiltroErrores(ILogger<FiltroErrores> logger)
        {
            this.logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}
