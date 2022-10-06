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
        public string username { get; init; }
        [Required]
        [MaxLength(20)]
        public string password { get; init; }
        [Required]
        [MaxLength(40)]
        public string emailAddress { get; init; }
        [Required]
        [MaxLength(20)]
        public string role { get; init; }
        public User() { }
        public User(string _username, string _password, string _emailAddress, string _role)
        {
            (username, password, emailAddress, role) = (_username, _password, _emailAddress, _role);
        }
    }
}
