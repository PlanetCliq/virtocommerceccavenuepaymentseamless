using Prometheus;

namespace VirtoCommerce.Payment.CCAvenue.Services
{
    public class PincodeMetricsCollector
    {
        private readonly Counter _apiSuccessCounter;
        private readonly Counter _apiFailureCounter;
        private readonly Counter _fallbackSuccessCounter;

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
        }

        public void RecordApiSuccess() => _apiSuccessCounter.Inc();
        public void RecordApiFailure() => _apiFailureCounter.Inc();
        public void RecordFallbackSuccess() => _fallbackSuccessCounter.Inc();
    }
}
[Fact]
public async Task CourierApi_ShouldRecordMetrics()
{
    var metrics = new PincodeMetricsCollector();

    // Simulate API success
    metrics.RecordApiSuccess();

    // Simulate API failure
    metrics.RecordApiFailure();

    // Simulate fallback success
    metrics.RecordFallbackSuccess();

    // Metrics are exposed via Prometheus /metrics endpoint
    // You can scrape and visualize them in Grafana
}
