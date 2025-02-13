using System.IO.Compression;
using System.Text.Json.Serialization;
using Produto.API.Middlewares;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.ResponseCompression;

namespace Produto.API.Extensions
{
    public static class CompressionExtension
    {
        private static readonly string[] Second =
        [
            "application/json",
            "application/xml",
            "text/plain",
            "image/png",
            "image/jpeg"
        ];

        public static IServiceCollection AddCompressionCustom(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                  options.EnableForHttps = true;
                  options.Providers.Add<GzipCompressionProvider>();
                  options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(Second);
                  options.Providers.Add<BrotliCompressionProvider>();
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.SmallestSize);
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/API/Controllers");
          
            return services;
        }
    }
}
