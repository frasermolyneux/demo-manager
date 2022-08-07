# Demo Manager

[![Build Status](https://dev.azure.com/frasermolyneux/XtremeIdiots-Public/_apis/build/status/demo-manager.OnePipeline?repoName=frasermolyneux%2Fdemo-manager&branchName=main)](https://dev.azure.com/frasermolyneux/XtremeIdiots-Public/_build/latest?definitionId=178&repoName=frasermolyneux%2Fdemo-manager&branchName=main)

## Overview

This repository contains the Demo Manager that is used by the [XtremeIdiots](https://www.xtremeidiots.com) gaming community. It is a desktop 'Click Once' application that enables players and admins to share demos that have been recorded from `Call of Duty 2`, `Call of Duty 4` and `Call of Duty: World at War`.

The demo manager uses APIs provided by the `xtremeidiots-portal` project to store the metadata and demo files.

---

## Related Projects

* [frasermolyneux/azure-landing-zones](https://github.com/frasermolyneux/azure-landing-zones) - The deploy service principal is managed by this project, as is the workload subscription.
* [frasermolyneux/xtremeidiots-portal](https://github.com/frasermolyneux/xtremeidiots-portal) - The xtremeidiots-portal project provides APIs for metadata and demo file storage.

---

## Solution

The solution is considered legacy at this point and only will be updated with features with the aim of decommissioning or security related changes.

---

## Pipelines

The `one-pipeline` is within the `.azure-pipelines` folder and output is visible on the [frasermolyneux/XtremeIdiots-Public](https://dev.azure.com/frasermolyneux/XtremeIdiots-Public/_build?definitionId=178) Azure DevOps project.
The `.github` folder contains `dependabot` configuration and some code quality workflows.

---

## Contributing

Please read the [contributing](CONTRIBUTING.md) guidance; this is a learning and development project.

---

## Security

Please read the [security](SECURITY.md) guidance; I am always open to security feedback through email or opening an issue.
