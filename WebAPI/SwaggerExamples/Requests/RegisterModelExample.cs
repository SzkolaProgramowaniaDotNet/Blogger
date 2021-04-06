using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Requests.SwaggerExamples
{
    public class RegisterModelExample : IExamplesProvider<RegisterModel>
    {
        public RegisterModel GetExamples()
        {
            return new RegisterModel
            {
                Username = $"yourUniqueName",
                Email = "yourEmailAddress@example.com",
                Password = "Pa$$w0rd123!"
            };
        }

    }
}
