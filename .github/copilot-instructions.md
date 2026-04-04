# Copilot Instructions

## Project Overview

Legacy Windows Forms ClickOnce client that discovers local Call of Duty 2/4/WaW installs, reads demo files, and uploads metadata and binaries to the XtremeIdiots portal APIs. The app is in maintenance/decommission mode.

## Tech Stack

- .NET Framework 4.8, WinForms UI
- Solution: `src/DemoManager.sln`
- Projects: `DemoManager.App` (UI), `DemoManager.Library` (shared parsing/helpers), `DemoManager.Debug` (diagnostics)
- NuGet packages restored via `packages.config` (Newtonsoft.Json, log4net)
- Infrastructure: Terraform in `terraform/` for Azure resource group and storage account

## Build & Run

- Open `src/DemoManager.sln` in Visual Studio and build (requires .NET Framework 4.8 SDK)
- Entry point: `DemoManager.App/Program.cs` launches `FrmMain`
- ClickOnce publish settings in `DemoManager.App.csproj` (`InstallUrl` points to blob storage)
- No automated tests; validate manually by opening demos and exercising upload/download flows

## Architecture & Key Flows

- **Game detection**: `Helpers/GameDetection.cs` uses registry lookup; `LocalDemoRepository` enumerates demos from installed games
- **Remote uploads**: `Repositories/RemoteDemoRepository` with auth key + base URL from `Configuration/DemoManagerConfiguration.cs`; progress via `UploadProgressChanged`
- **Demo parsing**: `Reader/DemoReader.cs`, `DemoMessage.cs`, and Huffman helpers extract config strings from demo binary files
- **Playback**: `Helpers/DemoPlayback.cs` starts game executables with demo arguments
- **Models**: `Objects/` contains `IDemo`, `LocalDemo`, `RemoteDemo`, `Game`, `Mod`, `UserMap`, `GameVersion`

## CI/CD

- GitHub Actions workflows in `.github/workflows/` for build-and-test, code quality, PR verification, deploy-prd, and dependabot automerge
- Infrastructure: Terraform in `terraform/` with `azurerm` provider, OIDC auth, backend in Azure Storage
- Terraform manages resource group and storage account for ClickOnce artifact hosting
- Deploy-prd workflow runs terraform plan+apply on push to main and weekly schedule

## Conventions

- Prefer ASCII encoding
- Avoid altering ClickOnce/publish settings unless necessary
- Keep auth headers (`demo-manager-auth-key`) and API endpoints consistent with the portal backend
- Logging via log4net; config at `DemoManager.App/log4net.config`
- Be cautious editing Huffman/bit-level parsing logic — small changes can break demo decoding
- Keep changes minimal; this is a maintenance-mode application