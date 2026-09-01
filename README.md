# VirtoCommerce CCAvenue Module

## Features
```
- Supports INR, USD, AED, SAR
- AES-256 encryption with retry logic
- SHA-256 checksum validation
- Timestamp validation (15-minute tolerance)
- BIN detection for Visa, MasterCard, RuPay, Amex
- Pincode serviceability check via JSON/CSV/TSV
- Webhook + Success/Failure controllers
- Logging with Serilog
- Monitoring with Application Insights + Prometheus + Grafana
- Unit testing with xUnit, Moq, AutoFixture, Coverlet
```

## Directory File Structure for Code as a Service inside VirtoCommerce on Microsoft Azure 1-click Deploy
[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fdravasp%2Fvirtocommerceccavenuepaymentseamless%2Fmain%2Fazuredeploy.json)

The link points to the Official VirtoCommerce ARM template published on the Azure Marketplace.

When clicked, Azure Portal opens with the VirtoCommerce template pre‑loaded, prompting you to fill in parameters like:
| Parameter              | Example Value              | Notes                                      |
|------------------------|----------------------------|--------------------------------------------|
| Resource Group         | virto-ccavenue-rg          | Logical container for all resources        |
| Region                 | Central India              | Closest Azure region to Mumbai             |
| App Service Plan       | virto-ccavenue-plan (S1)   | Standard tier compute plan                 |
| SQL Server & Database  | Server: virto-ccavenue-sql | Globally unique server name                |
|                        | Database: VirtoCommerce    | Default DB name                            |
| Storage Account        | virtostorage2026           | Blob storage for media/assets              |
| Search Service         | virto-search (Basic)       | Azure Cognitive Search for catalog indexing|
| Redis Cache            | virto-redis (C1 Standard)  | Provides session caching                   |
| Admin Email & Password | admin@yourdomain.com       | VirtoCommerce admin portal login           |
|                        | Adm1n!Virto2026            | Strong default password                    |


```
virtocommerceccavenuepaymentseamless/
 ├── Controllers/
 │   ├── CCAvenueController.cs
 │   ├── SuccessController.cs
 │   ├── FailureController.cs
 │   └── WebhookController.cs
 │
 ├── Services/
 │   ├── CCAvenuePaymentService.cs
 │   ├── CCAvenueChecksumValidator.cs
 │   ├── CCAvenueTimestampValidator.cs
 │   ├── CCAvenueResponseHandler.cs
 │   ├── CCAvenueTokenService.cs
 │   ├── CCAvenueCurrencyService.cs
 │   ├── CCAvenueBINProcessor.cs
 │   ├── PincodeValidator.cs
 │   └── PincodeMetricsCollector.cs
 │
 ├── Views/
 │   ├── Success.cshtml
 │   └── Failure.cshtml
 │
 ├── Data/
 │   └── pincodes.json
 │
 ├── Tests/
 │   ├── ControllersTests.cs
 │   ├── PincodeServiceabilityTests.cs
 │   ├── ResponseAndTokenTests.cs
 │   ├── ChecksumValidatorTests.cs
 │   ├── BINProcessorTests.cs
 │   ├── CurrencyServiceTests.cs
 │   └── PaymentServiceTests.cs
 │
 ├── Monitoring/
 │   └── GrafanaDashboard.json
 │
 ├── Module.cs
 ├── README.md
 └── composer.json
```


## Setup
```
1. Add module to VirtoCommerce.Platform.
2. Configure appsettings.json with MerchantId, WorkingKey, AccessCode, and Domain
3. Place pincode files in Data/pincode.json /pincode.csv /pincode.tsv
4. Build and deploy.
```

## Installation
1. Clone into `Modules/` directory of your VirtoCommerce installation:
   ```bash
   git clone https://github.com/dravasp/virtocommerceccavenuepaymentseamless Modules/VirtoCommerce.CCAvenue

```
To keep sensitive values like your **CCAvenue MerchantId, WorkingKey, and AccessCode** out of `appsettings.json`,
you can use **`secrets.json`** in .NET
```

# Using secrets.json in .NET

## Step 1: Initialize Secret Manager
Run this command in your project directory:
```bash
dotnet user-secrets init

```
This is part of the Secret Manager tool that stores configuration securely during development.

```
## Step 2: Add secrets
Use the CLI to store sensitive values:
dotnet user-secrets set "Payment:CCAvenue:MerchantId" "YOUR_MERCHANT_ID"
dotnet user-secrets set "Payment:CCAvenue:WorkingKey" "YOUR_32CHAR_WORKING_KEY"
dotnet user-secrets set "Payment:CCAvenue:AccessCode" "YOUR_ACCESS_CODE"
```

```
## Step 3: Bind in Program.cs
builder.Services.Configure<CCAvenueOptions>(
builder.Configuration.GetSection("Payment:CCAvenue"));

```
