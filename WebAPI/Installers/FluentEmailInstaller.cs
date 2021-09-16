using Application.Interfaces;
using Application.Services.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Installers
{
    public class FluentEmailInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services
                .AddFluentEmail(Configuration["FluentEmail:FromEmail"], Configuration["FluentEmail:FromName"])
                .AddRazorRenderer()
                .AddSmtpSender(Configuration["FluentEmail:SmtpSender:Host"],
                     int.Parse(Configuration["FluentEmail:SmtpSender:Port"]),
                               Configuration["FluentEmail:SmtpSender:Username"],
                               Configuration["FluentEmail:SmtpSender:Password"]);

            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
