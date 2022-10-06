using Application.Dtos.UserDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    //interface used for implementation of functions connected with auth tokens
    public interface ITokenService
    {
        public string GenerateJWT(UserDto userCredentials, IConfiguration config);
    }
}
