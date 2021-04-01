using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Wrappers;

namespace WebAPI.Attributes
{
    public class ValidateFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var entry = context.ModelState.Values.FirstOrDefault();

                context.Result = new BadRequestObjectResult(new Response<bool>
                {
                    Succeeded = false,
                    Message = "Something went wrong.",
                    Errors = entry.Errors.Select(x => x.ErrorMessage)
                });
            }
        }
    }
}
