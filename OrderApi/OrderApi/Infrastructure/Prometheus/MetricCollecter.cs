using System;
using Prometheus;

namespace OrderApi.Infrastructure.Prometheus
{
    public class MetricCollecter
    {
        private readonly Counter _requestCounter;
        private readonly Histogram _responseTimeHistogram;

        public MetricCollecter()
        {
            _requestCounter = Metrics.CreateCounter("total_requests", "The total number of requests serviced by this API.");

            _responseTimeHistogram = Metrics.CreateHistogram("request_duration_seconds", "The duration in seconds between the response to a request.", new HistogramConfiguration
            {
                Buckets = Histogram.ExponentialBuckets(0.01, 2, 10),
                LabelNames = new[]
                {
                    "status_code", "method"
                }
            });
        }

        public void RegisterRequest()
        {
            _requestCounter.Inc();
        }

        public void RegisterResponseTime(int statusCode, string method, TimeSpan elapsed)
        {
            _responseTimeHistogram.Labels(statusCode.ToString(), method).Observe(elapsed.TotalSeconds);
        }
    }
}