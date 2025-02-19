---
uid: troubleshooting-unattended-build
---

# Troubleshooting an unattended build

This article describes enabling logging and processing dumps in an unattended build on a build server without installing the `metalama` tool.

## Step 1. Create diagnostics.json on your local machine

You can follow the other articles in this chapter to learn how to create a `diagnostics.json` file for troubleshooting a specific scenario.

### Example: enabling logging

In the following example, you can find the resulting `diagnostics.json` file after editing it.

Here, logging is enabled for the compiler process and all categories.

```json
{
  "logging": {
    "processes": {
      "Compiler": true
    },
    "categories": {
      "*": true
    }
  }
}
```

## Step 2. Copy diagnostics.json to the METALAMA_DIAGNOSTICS environment variable

In your build or pipeline configuration, create an environment variable named `METALAMA_DIAGNOSTICS` and set its value to the content of the `diagnostics.json` file.

> [!WARNING]
> Using diagnostics set by environment variable always overrides local diagnostics settings used by the `metalama` tool.

## Step 3. Run the build on the build server

Metalama will automatically read the diagnostics configuration from an environment variable. The build will produce diagnostics based on the specified configuration set in the environment variable.

## Step 4. Download the logs

You will find them under the `%TEMP%\Metalama\Logs` directory.
