using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.HealthChecks
{
    public class ResponseTimeHealthCheck : IHealthCheck
    {
        private Random rnd = new Random();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int responseTimeMS = rnd.Next(1, 300);

            if (responseTimeMS < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"The response time looks good ({ responseTimeMS })"));
            }
            else if (responseTimeMS < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"The response time is a bit slow ({ responseTimeMS })"));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"The response time is unacceptable ({ responseTimeMS })"));
            }
        }
    }
}
