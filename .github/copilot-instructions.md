# Copilot Instructions

- Purpose: legacy Windows Forms ClickOnce client that discovers local Call of Duty 2/4/WaW installs, reads demo files, and uploads metadata + binaries to the XtremeIdiots portal APIs.
- Tech stack: .NET Framework 4.8, WinForms UI in `src/DemoManager.App`; shared parsing/helpers in `src/DemoManager.Library`; packages restored via `packages.config` (Newtonsoft.Json, log4net).
- Build/run: open `src/DemoManager.sln` in Visual Studio and build; primary entry point `DemoManager.App/Program.cs` launches `FrmMain`. ClickOnce settings live in `DemoManager.App.csproj` (`InstallUrl` points to blob storage).
- Key flows:
  - Local discovery uses `Helpers/GameDetection.cs` (registry lookup) and `LocalDemoRepository` to enumerate demos from installed games.
  - Uploads use `Repositories/RemoteDemoRepository` with auth key + base URL from `Configuration/DemoManagerConfiguration.cs`; progress surfaced via `UploadProgressChanged`.
  - Demo parsing lives in `Reader/DemoReader.cs`, `DemoMessage.cs`, and Huffman helpers to extract config strings from demo files.
  - Playback uses `Helpers/DemoPlayback.cs` to start game executables with demo arguments.
- Objects/models: see `Objects/` for `IDemo`, `LocalDemo`, `RemoteDemo`, `Game`, `Mod`, `UserMap`, `GameVersion`. Use `GameVersion.GetGame()` helpers to derive paths/extensions.
- Logging: log4net config at `DemoManager.App/log4net.config` (copied to output). Keep changes minimal—app is maintenance/decommission mode.
- Pipelines: GitHub workflows under `.github/workflows` handle code quality and PR validation; legacy Azure DevOps pipeline exists under `.azure-pipelines`.
- Testing: no automated tests; validate manually by opening demos and exercising upload/download flows. Be cautious editing Huffman/bit-level logic—small changes can break decoding.
- Conventions: prefer ASCII; avoid altering ClickOnce/publish settings unless necessary; keep auth headers (`demo-manager-auth-key`) and endpoints consistent with portal backend.