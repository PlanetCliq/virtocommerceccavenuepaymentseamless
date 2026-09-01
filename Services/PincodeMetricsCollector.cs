using Prometheus;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class PincodeMetricsCollector
    {
        private readonly Counter _apiSuccessCounter;
        private readonly Counter _apiFailureCounter;
        private readonly Counter _fallbackSuccessCounter;
        private readonly Counter _pincodeCheckedCounter;
        private readonly Histogram _apiLatencyHistogram;

        public PincodeMetricsCollector()
        {
            _apiSuccessCounter = Metrics.CreateCounter(
                "ccavenue_pincode_api_success_total",
                "Number of successful pincode checks via courier API");

            _apiFailureCounter = Metrics.CreateCounter(
                "ccavenue_pincode_api_failure_total",
                "Number of failed pincode checks via courier API");

            _fallbackSuccessCounter = Metrics.CreateCounter(
                "ccavenue_pincode_fallback_success_total",
                "Number of successful pincode checks via local fallback");

            _pincodeCheckedCounter = Metrics.CreateCounter(
                "ccavenue_pincode_checked_total",
                "Total pincodes checked (API + fallback)");

            _apiLatencyHistogram = Metrics.CreateHistogram(
                "ccavenue_pincode_api_latency_seconds",
                "Latency of courier API responses",
                new HistogramConfiguration
                {
                    Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.1, count: 20)
                });
        }

        public void RecordApiSuccess() => _apiSuccessCounter.Inc();
        public void RecordApiFailure() => _apiFailureCounter.Inc();
        public void RecordFallbackSuccess() => _fallbackSuccessCounter.Inc();
        public void RecordPincodeChecked() => _pincodeCheckedCounter.Inc();
        public void ObserveApiLatency(double seconds) => _apiLatencyHistogram.Observe(seconds);
    }
}
