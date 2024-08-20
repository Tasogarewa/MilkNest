namespace MilkNest.Server.Middleware
{
    public static class LocalizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseLocalizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
