# Demo Manager

[![Build and Test](https://github.com/frasermolyneux/demo-manager/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/frasermolyneux/demo-manager/actions/workflows/build-and-test.yml)
[![Code Quality](https://github.com/frasermolyneux/demo-manager/actions/workflows/codequality.yml/badge.svg)](https://github.com/frasermolyneux/demo-manager/actions/workflows/codequality.yml)
[![Copilot Setup Steps](https://github.com/frasermolyneux/demo-manager/actions/workflows/copilot-setup-steps.yml/badge.svg)](https://github.com/frasermolyneux/demo-manager/actions/workflows/copilot-setup-steps.yml)
[![Dependabot Automerge](https://github.com/frasermolyneux/demo-manager/actions/workflows/dependabot-automerge.yml/badge.svg)](https://github.com/frasermolyneux/demo-manager/actions/workflows/dependabot-automerge.yml)
[![Merge to Main](https://github.com/frasermolyneux/demo-manager/actions/workflows/merge-to-main.yml/badge.svg)](https://github.com/frasermolyneux/demo-manager/actions/workflows/merge-to-main.yml)
[![PR Verify](https://github.com/frasermolyneux/demo-manager/actions/workflows/pr-verify.yml/badge.svg)](https://github.com/frasermolyneux/demo-manager/actions/workflows/pr-verify.yml)

## Documentation

* [Docs Index](docs/index.md) - Entry point for project documentation.

## Overview

Legacy Windows desktop ClickOnce app for the XtremeIdiots gaming community to browse, upload, and replay Call of Duty 2/4/5 demos. Uses .NET Framework 4.8 WinForms with a shared `DemoManager.Library` for demo parsing (Huffman decode, config extraction) and local file handling. Integrates with the XtremeIdiots portal APIs to list and upload demos and with local game installs to detect paths and launch replays. The app is in maintenance/decommission mode; changes focus on security hygiene and dependency updates.

## Contributing

Please read the [contributing](CONTRIBUTING.md) guidance; this is a learning and development project.

## Security

Please read the [security](SECURITY.md) guidance; I am always open to security feedback through email or opening an issue.

## Local dev: MCP wire-up

This repo is wired to the shared `frasermolyneux-copilot` MCP server (org conventions catalog). The GitHub Copilot coding agent loads it via `.github/copilot/mcp_config.json`; `copilot-setup-steps.yml` checks out `frasermolyneux/.github-copilot` (pinned to `v0.1.0`) and builds the server (`npm ci && npm run build` in `.github-copilot/mcp-server`). For local clients (VS Code `.vscode/mcp.json`, Claude Desktop, Copilot CLI) and the full tool surface, see [`.github-copilot/mcp-server/README.md`](https://github.com/frasermolyneux/.github-copilot/blob/main/mcp-server/README.md).

