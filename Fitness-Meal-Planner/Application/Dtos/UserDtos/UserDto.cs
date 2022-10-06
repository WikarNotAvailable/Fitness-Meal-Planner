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
        public string username { get; init; }
        public string emailAddress { get; init; }
        public string role { get; init; }
        UserDto() { }
        UserDto(string _username, string _emailAddress, string _role)
        {
            (username, emailAddress, role) = (_username, _emailAddress, _role);
        }
    }
}
