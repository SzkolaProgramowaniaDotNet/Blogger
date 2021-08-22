using App.Metrics;
using App.Metrics.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Metrics
{
    public class MetricsRegistry
    {
        public static CounterOptions CreatedPostsCounter => new CounterOptions
        {
            Name = "Created Posts",
            Context = "bloggerapi",
            MeasurementUnit = Unit.Calls
        };
    }
}
