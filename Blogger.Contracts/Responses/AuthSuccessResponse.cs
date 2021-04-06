using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.Contracts.Responses
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
