namespace Produto.API.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClientCustom(this IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 4007;
            });
            return services;
        }
    }
}
