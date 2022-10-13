using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //Table holding users of the app
    [Table("Users")]
    public record User : AuditableEntity
    {
        [Key]
        [Required]
        [MaxLength(20)]
        public string Username { get; init; }
        [Required]
        [MaxLength(20)]
        public string Password { get; init; }
        [Required]
        [MaxLength(40)]
        public string EmailAddress { get; init; }
        [Required]
        [MaxLength(20)]
        public string Role { get; init; }
        public User() { }
        public User(string username, string password, string emailAddress, string role)
        {
            (Username, Password, EmailAddress, Role) = (username, password, emailAddress, role);
        }
    }
}
