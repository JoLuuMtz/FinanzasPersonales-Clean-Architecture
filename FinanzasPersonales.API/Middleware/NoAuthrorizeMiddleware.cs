using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinanzasPersonales.API
{
    public class NoAuthrorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public NoAuthrorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        // recibe el contexto http y devuelve un mensaje si no esta autorizado
        public async Task Invoke(HttpContext context)
        {
            // Mostrar una respuesta si no esta autorizado 
            var endpoint = context.GetEndpoint();

            // verfica si tiene el atributo de autorizacion
            if (endpoint != null && endpoint.Metadata.GetMetadata<AuthorizeAttribute>() != null)
            {
                // verfifica si esta loggeado o autoentido
                if (context.User.Identity.IsAuthenticated)
                {
                    // Continuar con el pipeline
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = 401;

                    var response = new
                    {
                        StatusCode = 401,
                        Message = "Necesita inicio de sesión"
                    };
                    // retorna la respuesta en formato json
                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }
            }

           
        }
    }
}
