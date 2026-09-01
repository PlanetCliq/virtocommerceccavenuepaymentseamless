# VirtoCommerce CCAvenue Module

## Features
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

## Setup
1. Add module to VirtoCommerce.Platform.
2. Configure appsettings.json with MerchantId, WorkingKey, AccessCode, and Domain
3. Place pincode files in Data/pincode.json /pincode.csv /pincode.tsv
4. Build and deploy.

## Installation
1. Clone into `Modules/` directory of your VirtoCommerce installation:
   ```bash
   git clone https://github.com/dravasp/virtocommerceccavenuepaymentseamless Modules/VirtoCommerce.CCAvenue

dotnet restore
dotnet build
