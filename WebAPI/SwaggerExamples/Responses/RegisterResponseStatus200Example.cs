using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Wrappers;

namespace WebAPI.SwaggerExamples.Responses
{
    public class RegisterResponseStatus200Example : IExamplesProvider<RegisterResponseStatus200>
    {
        public RegisterResponseStatus200 GetExamples()
        {
            return new RegisterResponseStatus200 
            { 
                Succeeded = true, 
                Message = "User created successfully!" 
            };
        }
    }

    public class RegisterResponseStatus200 : Response { }
}
