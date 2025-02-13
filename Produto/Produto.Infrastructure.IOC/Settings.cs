#nullable disable
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Produto.Infrastructure.IOC
{
    public class Settings
    {
        public ApplicationDetail ApplicationDetail { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Identity Identity { get; set; }
        public bool UseInMemoryDatabase { get; set; }
    }

    public class ApplicationDetail
    {
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public string ContactWebsite { get; set; }
    }

    public class ConnectionStrings
    {
        [Required]
        public string DefaultConnection { get; set; }
    }

    public class Identity
    {
        [Required]
        public string SecretKey { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }
        public bool ValidateHttps { get; set; }
    }

}
