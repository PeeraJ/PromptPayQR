# CLAUDE.md - AI Assistant Guide for PromptPayQR

## Project Overview

PromptPayQR is a C# .NET library for generating PromptPay QR codes compatible with Thailand's national e-payment system. PromptPay QR codes follow the EMVco standard and are accepted by major Thai banks including Bank of Ayudhya, Bangkok Bank, Kasikorn Bank, Krung Thai Bank, Siam Commercial Bank, and Thanachart Bank.

**License:** GNU General Public License V3
**Author:** Peera Jeawkok (Peera@live.it) - AppMan Co., Ltd
**Target Framework:** .NET Framework 4.7

## Repository Structure

```
PromptPayQR/
├── PromptPayQR/                    # Core library project
│   ├── PromptPayQR.cs              # Main QR code generation logic
│   ├── EMVcoConstants.cs           # EMVco specification constants
│   ├── CRC16.cs                    # CRC16-CCITT checksum implementation
│   ├── Properties/
│   │   └── AssemblyInfo.cs         # Assembly metadata
│   └── PromptPayQR.csproj          # Library project file
│
├── PromptPayQR.Demo/               # Demo console application
│   ├── Program.cs                  # Interactive console entry point
│   ├── DrawQR.cs                   # Windows console QR rendering
│   ├── PromptPayQR.sln             # Solution file (MAIN BUILD FILE)
│   ├── PromptPayQR.Demo.csproj     # Demo project file
│   └── Properties/
│       └── AssemblyInfo.cs
│
├── .travis.yml                     # Travis CI configuration
├── README.md                       # Project documentation
└── QR.PNG                          # Example QR code image
```

## Key Components

### Core Library (`PromptPayQR/`)

#### `PromptPayQR.cs`
Main class with three public methods:
- `QRCodePayload(string promptpayId, string amount)` - Generates EMVco-compliant payload string
- `QRCodeImage(string promptpayId, string amount)` - Returns Bitmap directly from ID/amount
- `QRCodeImage(string payload)` - Returns Bitmap from pre-generated payload

**PromptPay ID Types:**
- Phone numbers (10 digits, e.g., `0812345678`) - Converted to international format `0066812345678`
- Tax IDs (13 digits) - Used as-is

**POI Method Logic:**
- Empty amount = Static QR (can be reused)
- With amount = Dynamic QR (one-time use)

#### `EMVcoConstants.cs`
Constants following EMVco QR Code specification:
- Payload format identifiers (ID_PAYLOAD_FORMAT, ID_POI_METHOD, etc.)
- Thailand-specific values (COUNTRY_CODE_TH, TRANSACTION_CURRENCY_THB)
- Bank of Thailand GUID for PromptPay: `A000000677010111`

#### `CRC16.cs`
CRC16-CCITT implementation with polynomial `0x1021` and initial value `0xFFFF` for payload validation.

### Demo Application (`PromptPayQR.Demo/`)

Interactive console application demonstrating library usage:
- Prompts for PromptPay ID and amount
- Generates and displays QR code payload
- Renders QR bitmap directly in Windows console (uses kernel32.dll)

## Development Workflow

### Building the Project

```bash
# Open solution in Visual Studio
PromptPayQR.Demo/PromptPayQR.sln

# Or build via MSBuild/dotnet CLI
msbuild PromptPayQR.Demo/PromptPayQR.sln /p:Configuration=Debug
```

### Dependencies

- **QRCoder** - QR code generation library (external dependency, referenced from local path)
- **System.Drawing** - Bitmap handling (built-in .NET)

### CI/CD

Travis CI is configured for automated builds:
- Language: C#
- Solution: `./PromptPayQR.Demo/PromptPayQR.sln`

## Code Conventions

### Naming
- PascalCase for public methods and properties
- camelCase for local variables
- UPPER_SNAKE_CASE for constants

### String Formatting
- Two-digit length prefixes: `str.Length.ToString().PadLeft(2, '0')`
- Amounts formatted with 2 decimal places: `string.Format("{0:##.00}", amount)`
- CRC checksum as 4-character uppercase hex: `crc.ToString("X").PadLeft(4,'0')`

### Error Handling
- Minimal error handling in current implementation
- Phone number validation assumes 10 digits (starts with 0)
- Tax ID validation assumes 13+ digits

## Usage Examples

### Generate QR Code Payload
```csharp
// Phone number with amount
var payload = PromptPayQR.QRCodePayload("0812345678", "100.50");

// Tax ID without amount (static QR)
var payload = PromptPayQR.QRCodePayload("1234567890123", "");
```

### Generate QR Code Image
```csharp
// Direct bitmap generation
var bitmap = PromptPayQR.QRCodeImage("0812345678", "100.50");

// From existing payload
var bitmap = PromptPayQR.QRCodeImage(payload);
```

## Important Technical Notes

1. **EMVco Payload Structure**: The QR payload follows strict TLV (Tag-Length-Value) format per EMVco specification

2. **Phone Number Conversion**: Thai mobile numbers (starting with 0) are converted to international format with country code 66

3. **CRC Validation**: All payloads end with a 4-character CRC16 checksum that banks validate

4. **Merchant Information Length**: The library uses hardcoded length "37" for BOT merchant information block

5. **Windows-Only Demo**: The DrawQR utility uses Windows kernel32.dll for console rendering

## Potential Improvements (For AI Reference)

When modifying this codebase, consider:
- Input validation for phone numbers and tax IDs
- Cross-platform support (remove Windows-specific console rendering)
- Unit tests for payload generation and CRC calculation
- NuGet package support instead of local DLL reference
- Support for additional EMVco fields (merchant name, terminal ID, etc.)
