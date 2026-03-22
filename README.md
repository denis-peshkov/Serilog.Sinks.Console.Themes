# Serilog.Sinks.Console.Themes [![Nuget](https://img.shields.io/nuget/v/Serilog.Sinks.Console.Themes.svg)](https://nuget.org/packages/Serilog.Sinks.Console.Themes/) [![Documentation](https://img.shields.io/badge/docs-wiki-yellow.svg)](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/wiki)

A small companion library for [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console/) that defines **24-bit true-color** console themes (`38;2` / `48;2` escape sequences). Log output uses a fixed palette based on `System.Drawing.KnownColor` so you can tune colors in one place without hand-editing escape sequences.

**Target frameworks:** `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`

## Features

- **`CustomConsoleTheme.Dark`** / **`Light`** — convenience aliases for **`ConsoleThemes.Dark`** / **`Light`** (`ConsoleTheme` for `WriteTo.Console`).
- **`ConsoleThemes.UseTheme<T>()`** — build a theme from a **`BaseTheme`** template.
- **`ThemeStyle`** — fluent foreground, background, and SGR modes (`FormatTypeEnum`) for theme strings.
- **`TrueColor`** — low-level `KnownColor` / `ConsoleColor` / `Color` → `38;2` / `48;2` fragments, plus bold combinations for emphasis.

Prefer **static web colors** from `KnownColor` (for example `GhostWhite`, `CadetBlue`). OS-reserved `KnownColor` names (such as `ActiveCaption`) are rejected by `TrueColor` when resolved from `KnownColor` because they are not portable.

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
    .WriteTo.Console(theme: ConsoleThemes.Dark) // or CustomConsoleTheme.Dark
    .CreateLogger();

Log.Information("Hello, themed console");
```

Use `ConsoleThemes.Light` (or `CustomConsoleTheme.LightTheme`) for a light background.

### Preview (screenshot helper)

The **`Serilog.Sinks.Console.Themes.Demo`** sample logs every level plus structured properties, nulls, a different `SourceContext`, and an exception so you can compare themes in a real terminal:

```bash
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- dark
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- light
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- custom
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- sixteen
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- code
dotnet run --project Serilog.Sinks.Console.Themes.Demo --
```

The last line passes **no theme token** after `--` (same as an empty first argument). Together with **any unrecognized** token, that selects **`ConsoleTheme.None`** — plain console output without ANSI theme styling.

`custom` runs the sample **`MyTheme`** class (`ConsoleThemes.UseTheme<MyTheme>()`) from the Demo project. **`sixteen`** and **`code`** use built-in themes from **Serilog** (`AnsiConsoleTheme.Sixteen` and `AnsiConsoleTheme.Code` in assembly `Serilog.Sinks.Console`) for side-by-side comparison with this library’s themes.

#### Screenshots

The captures below were taken with those commands in a terminal that supports **24-bit color**. The demo prints every log level (Verbose through Fatal), structured scalars (string, number, boolean, enum, GUID, URI, date/time), `null` properties, a destructured object (`{@...}`), a line with a custom `SourceContext`, and an error with inner exception and stack trace. That combination shows how each theme tints the timestamp/level band, message text, property names, scalar kinds, and error vs. fatal highlighting.

**`CustomConsoleTheme.Dark`** — tuned for dark terminal backgrounds:

![Dark console sample](https://raw.githubusercontent.com/denis-peshkov/Serilog.Sinks.Console.Themes/master/img-dark.png)

**`CustomConsoleTheme.Light`** — tuned for light terminal backgrounds (same sample as above):

![Light console sample](https://raw.githubusercontent.com/denis-peshkov/Serilog.Sinks.Console.Themes/master/img-light.png)

You can combine a theme with your own `outputTemplate` as usual for the console sink.

## appsettings.json (`Serilog.Settings.Configuration`)

Reference [Serilog.Settings.Configuration](https://www.nuget.org/packages/Serilog.Settings.Configuration/) and wire `ReadFrom.Configuration` (for example via `UseSerilog` in ASP.NET Core, or `ReadFrom.Configuration(configuration)` when building the logger).

```xml
<PackageReference Include="Serilog.Settings.Configuration" Version="..." />
```

If you use central package management (`Directory.Packages.props`), declare the version there instead of on the reference.

`theme` is resolved from a **static property** using `Namespace.Type::MemberName, AssemblyName`, for example `Serilog.Sinks.Console.Themes.CustomConsoleTheme::DarkTheme, Serilog.Sinks.Console.Themes`, `::LightTheme`, or `Serilog.Sinks.Console.Themes.ConsoleThemes::Dark, Serilog.Sinks.Console.Themes`.

### This library (`CustomConsoleTheme.Dark`)

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

### Custom template (`ConsoleThemes.UseTheme<T>()`)

`Serilog.Settings.Configuration` resolves `theme` from a **static member** (`Type::Member, Assembly`). It cannot call the generic method `UseTheme<T>()` directly, so in **your** app define a static property (or field) that returns `ConsoleThemes.UseTheme<YourTemplate>()`:

```csharp
using System.Drawing;
using Serilog.Sinks.Console.Themes;
using Serilog.Sinks.SystemConsole.Themes;

namespace MyApp.Logging;

/// <summary>Example: tweak the built-in dark theme by subclassing <see cref="DarkTheme"/>.</summary>
public sealed class BrandDarkTheme : DarkTheme
{
    protected override string Text => ThemeStyle.Foreground(Color.Gold);
}

public static class LoggingThemes
{
    public static ConsoleTheme BrandDark { get; } = ConsoleThemes.UseTheme<BrandDarkTheme>();
}
```

Then reference that member from JSON. Include your **application assembly** in `Using` (assembly name is usually the project name, e.g. `MyApp`):

```json
{
  "Serilog": {
    "Using": [
      "Serilog.Sinks.Console",
      "Serilog.Sinks.Console.Themes",
      "MyApp"
    ],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "restrictedToMinimumLevel": "Verbose",
          "theme": "MyApp.Logging.LoggingThemes::BrandDark, MyApp",
          "outputTemplate": "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext:l}: {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

The template type must be **public** with a **parameterless constructor** (the same constraint as `UseTheme<T>()`). Subclass **`DarkTheme`**, **`LightTheme`**, or **`BaseTheme`** as needed.

### Themes defined in Serilog.Sinks.Console

Serilog ships additional ready-made themes; the `theme` string must use Serilog’s type names exactly (see [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console)):

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

Other static members on Serilog’s theme type (for example `Code`, `Grayscale`, `Literate`) use the same `Type::Member, Serilog.Sinks.Console` pattern.

## Custom theme (extend a base template)

1. Subclass **`BaseTheme`**, or subclass **`DarkThemeTemplate`** / **`LightThemeTemplate`** and override only the members you need.
2. Use **`ThemeStyle`** (or **`TrueColor`**) for escape fragments.
3. Register the theme:

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: ConsoleThemes.UseTheme<MyThemeTemplate>())
    .CreateLogger();
```

```csharp
public sealed class MyThemeTemplate : DarkThemeTemplate
{
    protected override string Text
        => ThemeStyle.Foreground(Color.Magenta);

    protected override string LevelDebug
        => ThemeStyle.Background(Color.CadetBlue).FormatType(FormatTypeEnum.BoldMode);

    protected override string Name
        => ThemeStyle.Style(Color.Salmon, Color.Azure);

    protected override string LevelError
        => ThemeStyle.Bold().Italic().Strikethrough().Foreground(Color.Red);
}
```

Configuration cannot call `ConsoleThemes.UseTheme<MyThemeTemplate>()` from JSON. Expose the result as a **static property** (or field) on a type in **your** assembly, then reference it with `TypeFullName::MemberName, AssemblyName` (same pattern as the **Custom template** section above).

### How to do it properly

In your application, declare for example:

```csharp
public static class LoggingThemes
{
    public static ConsoleTheme FromTemplate { get; } =
        ConsoleThemes.UseTheme<MyThemeTemplate>();
}
```

In `appsettings.json`:
```json
{
  "Serilog": {
    "Using": [
      "Serilog.Sinks.Console",
      "Serilog.Sinks.Console.Themes",
      "MyApp"
    ],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "restrictedToMinimumLevel": "Verbose",
          "theme": "MyApp.LoggingThemes::FromTemplate, MyApp",
          "outputTemplate": "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

Also add the `MyApp` assembly to the `Using` array.

## Customizing built-in palettes

Adjust the `KnownColor` constants in **`CustomConsoleTheme.DarkColors`** (dark) or **`LightColors`** (light). **`Dark`** and **`Light`** read those constants, so `ConsoleThemes.Dark` / `Light` pick up changes after recompilation.

## Terminal support

True-color themes need a terminal that supports **24-bit color**. On older or restricted hosts, output may be approximated or escape codes may be shown literally.

## Release notes

See [ReleaseNotes.md](ReleaseNotes.md).

## Contributing

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/your-change`).
3. Commit your changes with a clear message.
4. Push the branch and open a pull request.

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
