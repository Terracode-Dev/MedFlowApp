using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedFlow.Models
{
    public class login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
