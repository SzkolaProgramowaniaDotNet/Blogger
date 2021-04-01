using Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            #region Title

            RuleFor(x => x.Title).NotEmpty().WithMessage("Post can not have an empty title.");
            RuleFor(x => x.Title).Length(5, 100).WithMessage("The title must be between 5 and 100 characters long.");

            #endregion
        }
    }
}
