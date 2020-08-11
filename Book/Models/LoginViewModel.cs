using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Column("Email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Column("Password", TypeName = "nvarchar(100)")]
        public string Password { get; set; }
    }
}
