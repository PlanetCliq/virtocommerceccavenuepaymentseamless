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
| `Parameter`              | `Example Value`              | `Notes`                                      |
|------------------------|----------------------------|--------------------------------------------|
| `Resource Group`         | `virto-ccavenue-rg`          | `Logical container for all resources`        |
| `Region`                 | `Central India`              | `Closest Azure region to Mumbai`             |
| `App Service Plan`       | `virto-ccavenue-plan (S1)`   | `Standard tier compute plan`                 |
| `SQL Server & Database`  | `Server: virto-ccavenue-sql` | `Globally unique server name`                |
|                        | `Database: VirtoCommerce`    | `Default DB name`                            |
| `Storage Account`        | `virtostorage2026`           | `Blob storage for media/assets`              |
| `Search Service`         | `virto-search (Basic)`       | `Azure Cognitive Search for catalog indexing`|
| `Redis Cache`            | `virto-redis (C1 Standard)`  | `Provides session caching`                   |
| `Admin Email & Password` | `admin@yourdomain.com`       | `VirtoCommerce admin portal login`           |
|                        | `Adm1n!Virto2026`            | `Strong default password`                    |

Microsoft for Startups provides a range of technical benefits designed to help startups build and scale their businesses.

> Free Azure credits: Startups receive Azure credits to help them get started with building their solutions quickly and efficiently on the Azure platform.

>> Access to AI tools: Startups can leverage industry-leading AI services and tools for software development, enabling them to create innovative and intelligent applications.

>>> Technical resources: Startups have access to various resources that assist in building better products, scaling faster, and increasing sales. This includes documentation, guides

process:

```
> Visit Microsoft for Startups
Go to the official Microsoft for Startups India page at https://www.microsoft.com/en-in/startups and click Get started.
>> Create or sign in account
Use a personal Microsoft account (not work/school email) to sign in or create a new one.
>>> Provide business details
Enter your legal company name, industry, website, Mumbai address, and GSTIN if registered.
>>>> Verify identity
Complete OTP phone verification and provide a valid payment method.
>>>>>
Enter referral code from a VC or accelerator in Microsoft’s Investor Network, enter it during signup. 
>>>>>> Activate Azure credits
Standard signup gives ₹13,300 (~$200) credits; referral codes can unlock up to $150,000 credits.
>>>>>>> Deploy your solution
Use your credits to deploy VirtoCommerce with the 1‑Click Deploy button and configure secrets securely.

```

```
To provision resources, you can create an Azure Account with a valid payment method.
Azure Domain Group with IAM must be configured
Azure CLI installed locally to connect to your Azure Instance ID
- Install Azure CLI: `curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash`
- az login / - az account list

- Login to Admin Console for VirtoCommerce
- https://{replacedomain.com}.azurewebsites.net/admin >> Modules >> Installed Modules >> Configure Enable / Disable

Bind in Program.cs

builder.Services.Configure<CCAvenueOptions>(
    builder.Configuration.GetSection("Payment:CCAvenue"));

Run Tests by navigating to
cd site/wwwroot
cd Modules/VirtoCommerce.CCAvenue/Tests
dotnet test

(Optional) Generate coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

```
Modules/VirtoCommerce.CCAvenue/
 ├── Controllers/
 │   ├── CCAvenueController.cs
 │   ├── SuccessController.cs
 │   ├── FailureController.cs
 │   ├── CheckoutController.cs
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
