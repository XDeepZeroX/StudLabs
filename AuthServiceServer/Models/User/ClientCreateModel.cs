
using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;
namespace AuthServiceServer.Models
{
    public class ClientCreateModel
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public string ApiServices { get; set; }
    }
}
