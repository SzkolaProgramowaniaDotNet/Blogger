using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses
{
    public class RegisterResponseStatus500Example : IExamplesProvider<RegisterResponseStatus500>
    {
        public RegisterResponseStatus500 GetExamples()
        {
            return new RegisterResponseStatus500
            {
                Succeeded = false,
                Message = "User already exists!"
            };
        }
    }

    public class RegisterResponseStatus500 : Response { }
}
