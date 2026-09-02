# Strict Security & Compliance Policy Framework

## 1. Prohibited Uses
- **Fraudulent or unlawful activity**: Misuse packages or services for spamming, phishing, bypassing billing, or unauthorized access.  
- **Network compromise**: Deploy code or configurations that degrade Azure/Microsoft cloud security, GitHub workflows, or package registries.  
- **Unauthorized redistribution**: Resell, repackage, or transfer NuGet/Packagist/Composer modules without author consent.  
- **Improper integration**: Use libraries in unsupported devices, geographies, or regulatory environments.  
- **Promotional abuse**: Exploit GitHub Actions, Azure credits, or marketplace promotions to avoid payment obligations.  

---

## 2. Operator & Platform Rights
Platform providers (Microsoft, GitHub, NuGet, Packagist, Composer, Azure) may suspend or revoke access without prior notice if:
- **Fraud detection**: Unusual usage patterns or malicious commits are detected.  
- **Credential compromise**: Tokens, API keys, or SIM/eSIM equivalents are reported stolen or misused.  
- **Regulatory non-compliance**: Failure to meet KYC, export control, or telecom/IT law requirements.  
- **Service interference**: Code or devices disrupt GitHub runners, NuGet feeds, or Azure services.  
- **Extended inactivity**: Accounts remain unused for >6 months.  
- **Legal breach**: Violation of local laws or platform terms.  

---

## 3. Customer & Developer Responsibilities
- **Secure access**: Protect GitHub/Azure credentials, enforce MFA, and secure NuGet/Composer tokens.  
- **Report misuse**: Immediately notify platform support of suspected fraud or unauthorized use.  
- **Compliance**: Ensure adherence to local telecom, IT, and data protection regulations.  
- **Liability**: Remain liable for all charges until platform confirms suspension in writing.  

---

## 4. Revocation & Recovery
- **Reinstatement**: Suspended services may be reinstated within 3 months subject to compliance.  
- **Permanent revocation**: Severe or repeated violations may result in permanent termination.  
- **Non-transferable credits**: GitHub/Azure credits or allowances are non-refundable unless mandated by law.  

---

## 5. Legal & Liability Framework
- **Liability disclaimer**: Providers disclaim responsibility for outages, third-party disruptions, or interception risks.  
- **Indemnity**: Customers indemnify providers against claims arising from misuse of services or packages.  
- **Jurisdiction**:  
  - Domestic: India (Mumbai jurisdiction).  
  - Foreign providers: Home jurisdiction applies.  

---

## 6. Package-Specific Security Controls
- **VirtoCommerce.Payment / VirtoCommerce.Platform.Core**: Validate payment flows, enforce PCI DSS compliance.  
- **Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation**: Restrict runtime compilation to trusted sources.  
- **Newtonsoft.Json**: Sanitize deserialization inputs to prevent injection.  
- **FluentValidation**: Enforce strict input validation.  
- **Polly**: Apply resilience policies without exposing sensitive retry logic.  
- **Refit / RestSharp**: Secure API calls with TLS and OAuth.  
- **IdentityModel**: Enforce token validation and expiry checks.  
- **Serilog / Serilog.Sinks.Console / Serilog.Sinks.File**: Mask sensitive data in logs.  
- **Microsoft.ApplicationInsights.AspNetCore**: Ensure telemetry does not leak PII.  
- **prometheus-net**: Restrict metrics endpoints to internal networks.  
- **CsvHelper**: Validate CSV inputs to prevent injection.  
- **System.IO.Abstractions**: Restrict file system access to permitted paths.  
- **xunit / xunit.runner.visualstudio / Microsoft.NET.Test.Sdk**: Ensure test data does not expose secrets.  
- **Moq / AutoFixture**: Prevent mocking of sensitive services.  
- **coverlet.collector**: Ensure coverage reports exclude sensitive code paths.  

---

## 7. Ecosystem Compliance
- **GitHub**: Enforce branch protection, signed commits, and secret scanning.  
- **NuGet**: Verify package signatures and maintain dependency hygiene.  
- **Azure**: Apply RBAC, network isolation, and encryption at rest/in transit.  
- **Microsoft**: Adhere to Secure Development Lifecycle (SDL) practices.  
- **Packagist / Composer**: Validate package integrity and avoid unverified sources.  
