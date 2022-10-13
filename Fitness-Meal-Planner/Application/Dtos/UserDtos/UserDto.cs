using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    //dto used for showing info of user
    public record UserDto
    {
        public string Username { get; init; }
        public string EmailAddress { get; init; }
        public string Role { get; init; }
        UserDto() { }
        UserDto(string username, string emailAddress, string role)
        {
            (Username, EmailAddress, Role) = (username, emailAddress, role);
        }
    }
}
