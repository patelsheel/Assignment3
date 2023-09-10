namespace Assignment3.Middleware
{
    public static class MiddlewareExtensionMethod
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<CustomMiddleware>();
        }
    }
}
