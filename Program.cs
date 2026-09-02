using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtoCommerce.Payment.CCAvenue;
using Prometheus; // <-- add this

var builder = WebApplication.CreateBuilder(args);

// 1. Add controllers
builder.Services.AddControllers();

// 2. Bind CCAvenueOptions from config (appsettings.json, env vars, or user-secrets)
builder.Services.Configure<CCAvenueOptions>(
    builder.Configuration.GetSection("Payment:CCAvenue"));

// 3. Register your payment service
builder.Services.AddScoped<CCAvenuePaymentService>();

// 4. Add logging, monitoring, etc.
builder.Services.AddLogging();
builder.Services.AddHealthChecks();

var app = builder.Build();

// 5. Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();

// 6. Map controllers and health checks
app.MapControllers();
app.MapHealthChecks("/health");

// 7. Expose Prometheus metrics endpoint
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics(); // exposes /metrics for Prometheus scraping
});

app.Run();
