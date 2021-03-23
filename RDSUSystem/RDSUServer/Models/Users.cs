using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool Status { get; set; }
    }
}
