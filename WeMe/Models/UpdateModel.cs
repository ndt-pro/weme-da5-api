using Microsoft.AspNetCore.Http;

namespace WebApi.Models.Users
{
  public class UpdateModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile Avatar { get; set; }
    }
}