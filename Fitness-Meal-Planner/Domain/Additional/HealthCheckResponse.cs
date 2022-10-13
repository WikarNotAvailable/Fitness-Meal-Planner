using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Additional
{
    public class HealthCheckResponse
    {
        public string status { get; set; }
        public IEnumerable<IndividualHealthCheckResponse> healthChecks { get; set; }
        public TimeSpan healthCheckDuration { get; set; }
    }
}
