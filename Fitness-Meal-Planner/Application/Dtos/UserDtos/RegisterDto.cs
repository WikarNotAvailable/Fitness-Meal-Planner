using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public record RegisterDto
    {
        public string username { get; init; }
        public string password { get; init; }
        public string emailAddress { get; init; }
        public string role { get; init; }
    }
}
