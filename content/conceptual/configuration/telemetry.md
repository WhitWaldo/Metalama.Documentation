---
uid: telemetry
---

# Telemetry

This article describes what data Metalama collects and how you can change its settings.

## What is being collected?

By default, Metalama will collect and send to PostSharp Technologies usage and quality reports. Telemetry reports are anonymous and collected in the following situations:

- In case of an unexpected _failure_ or _performance degradation_ that is not caused by user code. In this case, an exception report including the anonymized call stack is reported.
- Periodically, for each project you are building, we collect data such as a one-way hash of the project name, the target framework and version, the project size, number of aspects used, amount of code saved by Metalama, or performance.

All reports include a randomly generated device id, which you can [reset at any time using Metalama Command Line Tools](#resetting-your-device-id).

Telemetry data is collected and processed according to our [Privacy Policy](https://www.postsharp.net/company/legal/privacy-policy).

### License audit

In addition to telemetry, the use of the software may be subject to a _license audit_. License audit is anonymous but mandatory for Metalama Free and the self-generated Metalama Trial, and is used to provide statistics on the number of users. If you are using a license key, license audit reports include the id of your license key. If you do not agree with license audit, please [contact our sales team](mailto:hello@postsharp.net) and we will provide you with a new license key including a license audit waiver flag.

## Disabling telemetry

You can disable telemetry using one of the two following methods.

### Option 1. Defining an environment variable

You can disable telemetry by defining the `METALAMA_TELEMETRY_OPT_OUT` environment variable to any non-empty value.

Using this approach, you can disable telemetry for build agents, or you can disable telemetry for all devices in your domain using remote management tools such as Azure Endpoint Manager.

### Option 2. Using Metalama Command Line Tools

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama telemetry disable
   ```

## Resetting your device id

Metalama Telemetry uses a randomly generated GUID to uniquely identify your device. You can reset this ID at any time. After you reset your ID, PostSharp Technologies will no longer be able to correlate past and future reports.

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama telemetry reset-device-id
   ```
