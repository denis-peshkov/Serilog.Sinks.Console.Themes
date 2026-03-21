# Serilog.Sinks.Console.Themes [![Nuget](https://img.shields.io/nuget/v/Serilog.Sinks.Console.Themes.svg)](https://nuget.org/packages/Serilog.Sinks.Console.Themes/) [![Documentation](https://img.shields.io/badge/docs-wiki-yellow.svg)](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/wiki)

A small companion library for [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console/) that defines a **24-bit ANSI** (`38;2` / `48;2`) console theme. Log output uses a fixed palette based on `System.Drawing.KnownColor` so you can tune colors in one place without hand-editing escape sequences.

**Target frameworks:** `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`

## Features

- **`CustomConsoleTheme.DarkTheme`** — an `AnsiConsoleTheme` wired for text, property names, scalars, and log levels (including distinct error/fatal styling).
- **`TrueColor`** — helpers that map `KnownColor` or `ConsoleColor` to foreground/background ANSI fragments, plus bold combinations for emphasis.

Prefer **static web colors** from `KnownColor` (for example `GhostWhite`, `CadetBlue`). System UI colors (such as `ActiveCaption`) are rejected by `TrueColor` because they are not portable.

## Install NuGet package

Install the _Serilog.Sinks.Console.Themes_ [NuGet package](https://www.nuget.org/packages/Serilog.Sinks.Console.Themes/) into your .NET project:

```powershell
Install-Package Serilog.Sinks.Console.Themes
```
or
```bash
dotnet add package Serilog.Sinks.Console.Themes
```

## Quick start

```csharp
using Serilog;
using Serilog.Sinks.Console.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: CustomConsoleTheme.DarkTheme)
    .CreateLogger();

Log.Information("Hello, themed console");
```

You can combine a theme with your own `outputTemplate` as usual for the console sink.

## appsettings.json (`Serilog.Settings.Configuration`)

Reference [Serilog.Settings.Configuration](https://www.nuget.org/packages/Serilog.Settings.Configuration/) and wire `ReadFrom.Configuration` (for example via `UseSerilog` in ASP.NET Core, or `ReadFrom.Configuration(configuration)` when building the logger).

```xml
<PackageReference Include="Serilog.Settings.Configuration" Version="..." />
```

If you use central package management (`Directory.Packages.props`), declare the version there instead of on the reference.

`theme` is resolved from a **static property** using `Namespace.Type::MemberName, AssemblyName`, for example `Serilog.Sinks.Console.Themes.CustomConsoleTheme::DarkTheme, Serilog.Sinks.Console.Themes`.

### This library (`CustomConsoleTheme.DarkTheme`)

List the theme assembly in `Using` so configuration can load the type:

```json
{
  "Serilog": {
    "Using": [
      "Serilog.Sinks.Console",
      "Serilog.Sinks.Console.Themes"
    ],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "restrictedToMinimumLevel": "Verbose",
          "theme": "Serilog.Sinks.Console.Themes.CustomConsoleTheme::DarkTheme, Serilog.Sinks.Console.Themes",
          "outputTemplate": "{Timestamp:HH:mm:ss.fff zzz} CorrelationId:[{CorrelationId}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

The `outputTemplate` uses `{CorrelationId}` only if you add that property to the log context (for example with an enricher or `LogContext.PushProperty`); otherwise remove or replace that segment.

### Built-in `AnsiConsoleTheme` (Serilog.Sinks.Console)

For comparison, built-in themes ship in the **Serilog.Sinks.Console** assembly:

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console" ],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "restrictedToMinimumLevel": "Verbose",
          "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Sixteen, Serilog.Sinks.Console",
          "outputTemplate": "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

Other static members on `AnsiConsoleTheme` (for example `Code`, `Grayscale`, `Literate`) work the same way: `Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console`.

## Customizing colors

Adjust the `KnownColor` constants in `CustomConsoleTheme.DarkColors`. `DarkTheme` is built once from those values via `TrueColor.Foreground`, `TrueColor.Background`, and `TrueColor.BoldForegroundBackground` for fatal-level lines.

## Terminal support

True-color themes need a terminal that supports **24-bit ANSI color**. On older or restricted consoles, colors may be approximated or ignored depending on the host.

## Release notes

See [ReleaseNotes.md](ReleaseNotes.md).

## Contributing

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/your-change`).
3. Commit your changes with a clear message.
4. Push the branch and open a pull request.

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
