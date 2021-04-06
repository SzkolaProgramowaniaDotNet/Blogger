using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Wrappers
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
