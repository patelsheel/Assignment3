namespace Assignment3.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class CustomMiddleware
    {
        private readonly RequestDelegate next;

        public CustomMiddleware(RequestDelegate Next)
        {
            next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Custom-Middleware", "Hello from Custom Middleware");
            await next(context);
            return;
        }
    }
}


