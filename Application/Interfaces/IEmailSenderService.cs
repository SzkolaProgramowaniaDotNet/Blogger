using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailSenderService
    {
        Task<bool> Send(string to, string subject, EmailTemplate template, object model);
    }
}
