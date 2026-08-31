# AGENTS.md — demo-manager

Repository `demo-manager` in the frasermolyneux organization: a legacy .NET Framework 4.8 WinForms ClickOnce client (maintenance/decommission mode) plus Terraform for its Azure hosting.

This file is the brief for the **GitHub Copilot coding agent** (and any other agent that follows the [agents.md](https://agents.md) convention) when it runs in a cloud runner.

> Prefer `.github/copilot-instructions.md` for architecture, tech stack, and conventions — read it first. This file covers agent execution: commands, guardrails, and escalation.

---

## Build, test, format

```pwsh
# Restore & build — Windows only, requires MSBuild + the NuGet CLI (VS Build Tools).
# This is a .NET Framework 4.8, non-SDK-style solution; there is no `dotnet build` equivalent.
nuget restore src/DemoManager.sln
msbuild src/DemoManager.sln /p:Configuration=Release /p:Platform="Any CPU"
```

- No automated test suite exists; validate manually per `README.md` / `docs/index.md`.
- There is no format-check step in CI for the .NET code; do not add one.

```pwsh
# Terraform — single "prd" environment, no dev
terraform -chdir=terraform fmt -check -recursive
terraform -chdir=terraform init -backend-config=backends/prd.backend.hcl
terraform -chdir=terraform validate
terraform -chdir=terraform plan -var-file=tfvars/prd.tfvars
```

---

## Do NOT

- ❌ Do not introduce client secrets, connection strings, or hard-coded subscription IDs/GUIDs. Terraform auth is OIDC + managed identity; never hard-code the app's `demo-manager-auth-key`.
- ❌ Do not change Azure resource naming or tagging conventions without explicit instruction.
- ❌ Do not modify `.github/workflows/`, `.github/dependabot.yml`, `version.json`, or the `platform_monitoring` / `platform_workloads` remote-state wiring in `terraform/data.remote_state.tf` unless that is the explicit task.
- ❌ Do not assume a Windows/MSBuild toolchain is available in this runner; if the build can't be validated here, say so rather than guessing at results.
