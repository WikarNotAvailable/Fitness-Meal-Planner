using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public record LoginDto
    {
        public string username { get; init; }
        public string password { get; init; }
    }
}
