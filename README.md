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
