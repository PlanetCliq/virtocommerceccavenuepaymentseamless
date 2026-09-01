using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Payment.CCAvenue.Services;
using Serilog;

namespace VirtoCommerce.Payment.CCAvenue
{
    public class Module : IModule, IHasConfiguration
    {
        public ManifestModuleInfo ModuleInfo { get; set; }

        public void Initialize(IServiceCollection services)
        {
            // Core services
            services.AddTransient<CCAvenuePaymentService>();
            services.AddTransient<CCAvenueResponseHandler>();
            services.AddTransient<CCAvenueTokenService>();

            // Validators & processors
            services.AddSingleton<CCAvenueChecksumValidator>();
            services.AddSingleton<CCAvenueTimestampValidator>();
            services.AddSingleton<CCAvenueCurrencyService>();
            services.AddSingleton<CCAvenueBINProcessor>();
            services.AddSingleton<PincodeValidator>();
            services.AddSingleton<PincodeMetricsCollector>();

            // Logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/ccavenue.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void PostInitialize(IApplicationBuilder appBuilder)
        {
            // Optional: configure middleware or endpoints here if needed
        }

        public void Uninstall()
        {
            // Optional: cleanup logic if required
        }
    }
}
