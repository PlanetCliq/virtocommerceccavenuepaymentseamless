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
- Monitoring with Application Insights + Prometheus
- Unit testing with xUnit, Moq, AutoFixture, Coverlet

## Setup
1. Add module to VirtoCommerce.Platform.
2. Configure appsettings.json with MerchantId, WorkingKey, AccessCode.
3. Place pincode files in Data/.
4. Build and deploy.
