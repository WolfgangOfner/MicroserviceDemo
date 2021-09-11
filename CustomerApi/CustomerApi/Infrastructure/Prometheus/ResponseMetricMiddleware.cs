using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CustomerApi.Infrastructure.Prometheus
{
    public class ResponseMetricMiddleware
    {
        private readonly RequestDelegate _request;

        public ResponseMetricMiddleware(RequestDelegate request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task Invoke(HttpContext httpContext, MetricCollector collector)
        {
            var path = httpContext.Request.Path.Value;

            if (path == "/metrics")
            {
                await _request.Invoke(httpContext);

                return;
            }

            var sw = Stopwatch.StartNew();

            try
            {
                await _request.Invoke(httpContext);
            }
            finally
            {
                sw.Stop();
                
                collector.RegisterRequest();
                collector.RegisterResponseTime(httpContext.Response.StatusCode, httpContext.Request.Method, sw.Elapsed);
            }
        }
    }
}