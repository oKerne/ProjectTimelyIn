using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ProjectTimelyIn.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ShabbatMiddleware
    {
        private readonly RequestDelegate _next;

        public ShabbatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("The service is not active on Shbuhs.");
            }
            else
            {
                await _next(context);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ShabbatMiddlewareExtensions
    {
        public static IApplicationBuilder UseShabbatMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShabbatMiddleware>();
        }
    }
}
