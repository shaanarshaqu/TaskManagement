using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class LoginCredentials
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
