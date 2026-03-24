# Serilog.Sinks.Console.Themes [![NuGetVersion](https://img.shields.io/nuget/v/Serilog.Sinks.Console.Themes.svg)](https://nuget.org/packages/Serilog.Sinks.Console.Themes/) [![NugetDownloads](https://img.shields.io/nuget/dt/Serilog.Sinks.ApplicationInsights.AspNetCore.svg)](https://nuget.org/packages/Serilog.Sinks.ApplicationInsights.AspNetCore/) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Serilog.Sinks.ApplicationInsights.AspNetCore&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Serilog.Sinks.ApplicationInsights.AspNetCore) [![.NET PR](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/actions/workflows/dotnet.yml/badge.svg?event=pull_request)](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/actions/workflows/dotnet.yml)

A small companion library for [Serilog.Sinks.Console](https://www.nuget.org/packages/Serilog.Sinks.Console/) that defines **24-bit true-color** console themes (`38;2` / `48;2` escape sequences). Log output uses a fixed palette based on `System.Drawing.KnownColor` so you can tune colors in one place without hand-editing escape sequences.

**Target frameworks:** `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`

## Features

- **`CustomConsoleTheme`** — cached **`Dark`** / **`Light`** presets, palette constants (**`DarkColors`** / **`LightColors`**), and **`UseTheme<T>()`** for a **`ConsoleTheme`**.
- **`CustomTemplateTheme.Dark`** / **`Light`** and **`CustomTemplateTheme.UseTheme<T>()`** — **`TemplateTheme`** presets parallel to **`CustomConsoleTheme`** (requires **`Serilog.Expressions`**).
- **`ThemeStyle`** — fluent foreground, background, and SGR modes (`FormatTypeEnum`) for theme strings (24-bit fragments via **`TrueColorConverter`**).
- **`BaseTheme`**, **`DarkTheme`**, **`LightTheme`**, **`ThemeStyleConverter`** — template strings and conversion from **`ConsoleTheme`** styles to **`TemplateTheme`** for **`ExpressionTemplate`**.
- **`TrueColorConverter`** — low-level `KnownColor` / `ConsoleColor` / `Color` → `38;2` / `48;2` fragments, plus bold combinations for emphasis.

Prefer **static web colors** from `KnownColor` (for example `GhostWhite`, `CadetBlue`). OS-reserved `KnownColor` names (such as `ActiveCaption`) are rejected by `TrueColorConverter` when resolved from `KnownColor` because they are not portable.

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

In your application:

```csharp
using Serilog;
using Serilog.Sinks.Console.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: CustomConsoleTheme.Dark)
    .CreateLogger();

Log.Information("Hello, themed console");
```

Use **`CustomConsoleTheme.Light`** for a light background.

To configure the logger from `appsettings.json`, add a reference to [Serilog.Settings.Configuration](https://www.nuget.org/packages/Serilog.Settings.Configuration/) and wire `ReadFrom.Configuration` (for example via `UseSerilog` in ASP.NET Core, or `ReadFrom.Configuration(configuration)` when building the logger).

`theme` is resolved from a **static property** using `Namespace.Type::MemberName, AssemblyName`. For built-in presets use **`Serilog.Sinks.Console.Themes.CustomConsoleTheme::Dark, Serilog.Sinks.Console.Themes`** (or `::Light`) — same names as in code, and distinct from Serilog’s **`AnsiConsoleTheme::…`** strings.


### Preview (screenshot helper)

The **`Serilog.Sinks.Console.Themes.Demo`** project lets you compare themes in a terminal that supports **24-bit color**. The demo prints every log level (Verbose through Fatal), structured scalars (string, number, boolean, enum, GUID, URI, date/time), `null` properties, a destructured object (`{@...}`), a line with a custom `SourceContext`, and an error with inner exception and stack trace — so you can see how each theme tints the timestamp/level band, message text, property names, scalar kinds, and error vs. fatal highlighting.

```bash
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- dark
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- light
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- custom
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- sixteen
dotnet run --project Serilog.Sinks.Console.Themes.Demo -- code
dotnet run --project Serilog.Sinks.Console.Themes.Demo --
```

The last line passes **no theme token** after `--` (same as an empty first argument). Together with **any unrecognized** token, that selects **`ConsoleTheme.None`** — plain console output without ANSI theme styling.

`custom` runs the sample **`MyBrandTheme`** class (`CustomConsoleTheme.UseTheme<MyBrandTheme>()`) from the Demo project. **`sixteen`** and **`code`** use built-in themes from **Serilog** (`AnsiConsoleTheme.Sixteen` and `AnsiConsoleTheme.Code` in assembly `Serilog.Sinks.Console`) for side-by-side comparison with this library’s themes.

#### Screenshots

Captures below use the same demo commands.

**`CustomConsoleTheme.Dark`** — tuned for dark terminal backgrounds:

![Dark console sample](https://raw.githubusercontent.com/denis-peshkov/Serilog.Sinks.Console.Themes/master/img-dark.png)

**`CustomConsoleTheme.Light`** — tuned for light terminal backgrounds (same sample as above):

![Light console sample](https://raw.githubusercontent.com/denis-peshkov/Serilog.Sinks.Console.Themes/master/img-light.png)

You can combine a theme with your own `outputTemplate` as usual for the console sink.

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
          "theme": "Serilog.Sinks.Console.Themes.CustomConsoleTheme::Dark, Serilog.Sinks.Console.Themes",
          "outputTemplate": "{Timestamp:HH:mm:ss.fff zzz} CorrelationId:[{CorrelationId}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

The `outputTemplate` uses `{CorrelationId}` only if you add that property to the log context (for example with an enricher or `LogContext.PushProperty`); otherwise remove or replace that segment.

### Custom template (`CustomConsoleTheme.UseTheme<T>()`)

`Serilog.Settings.Configuration` resolves `theme` from a **static member** (`Type::Member, Assembly`). It cannot call the generic method `UseTheme<T>()` directly, so in **your** app define a static property (or field) that returns `CustomConsoleTheme.UseTheme<YourTemplate>()`:

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
    public static ConsoleTheme BrandDark { get; } = CustomConsoleTheme.UseTheme<BrandDarkTheme>();
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

1. Subclass **`BaseTheme`**, or subclass **`DarkTheme`** / **`LightTheme`** and override only the members you need.
2. Use **`ThemeStyle`** (or **`TrueColorConverter`**) for escape fragments.
3. Register the theme:

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: CustomConsoleTheme.UseTheme<MyBrandTheme>())
    .CreateLogger();
```

```csharp
public sealed class MyBrandTheme : DarkTheme
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

For **appsettings.json**, use the same **static member** pattern as in **Custom template (`CustomConsoleTheme.UseTheme<T>()`)** above (expose `CustomConsoleTheme.UseTheme<MyBrandTheme>()` on your type, then `TypeFullName::MemberName, AssemblyName` in JSON).

### Example (static property + `appsettings.json`)

In your application, declare for example (namespace + class must match the `theme` type in JSON — here **`MyApp.LoggingThemes`**):

```csharp
using Serilog.Sinks.Console.Themes;
using Serilog.Sinks.SystemConsole.Themes;

namespace MyApp;

public static class LoggingThemes
{
    public static ConsoleTheme FromTemplate { get; } =
        CustomConsoleTheme.UseTheme<MyBrandTheme>();
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

## Expressions with `ExpressionTemplate` and `TemplateTheme`

The **`ExpressionTemplate`** type, **`TemplateTheme`**, and the **`Serilog.Templates`** APIs live in the **[Serilog.Expressions](https://www.nuget.org/packages/Serilog.Expressions/)** package — this repo **extends** that stack with **24-bit true-color** `TemplateTheme` presets (**`CustomTemplateTheme`**, **`ThemeStyleConverter`**, and template helpers built on **`BaseTheme`**).

When you format the console with **`Serilog.Expressions`** instead of `outputTemplate`, pass a **`TemplateTheme`** into **`ExpressionTemplate`** (or `TryParse`). The same palettes are available as **`CustomConsoleTheme.Dark`** / **`Light`** (console) and **`CustomTemplateTheme.Dark`** / **`Light`** (template):

```csharp
using Serilog;
using Serilog.Sinks.Console;
using Serilog.Sinks.Console.Themes;
using Serilog.Templates;

var formatter = new ExpressionTemplate(
    "{@t:HH:mm:ss} [{@l:u3}] {@m}\n",
    formatProvider: null,
    nameResolver: null,
    theme: CustomTemplateTheme.Dark,
    applyThemeWhenOutputIsRedirected: true,
    encoder: null);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatter)
    .CreateLogger();
```

### Optional parameters

The **`ExpressionTemplate`** constructor (and the full **`TryParse`** overload) take several arguments besides the template text and theme:

- **`formatProvider`** — `IFormatProvider` for culture-specific formatting of values in the template (dates, numbers). **`null`** uses default formatting (typically the current culture).
- **`nameResolver`** — Resolves custom function names in the template. **`null`** means only built-in functions; pass a **`NameResolver`** when you register app-specific template functions.
- **`applyThemeWhenOutputIsRedirected`** — When **`false`**, Serilog may omit theme ANSI sequences if the console output is redirected (file, pipe, some test hosts), so logs stay plain text. When **`true`**, the theme is applied anyway—useful when the consumer still expects ANSI escapes.
- **`encoder`** — Optional **`TemplateOutputEncoder`** for transforming or sanitizing substituted output. **`null`** applies no extra encoding.

Reference **`Serilog.Expressions`** (already pulled transitively when you use `ExpressionTemplate`). Override colors on a **`BaseTheme`** subclass and call **`CustomTemplateTheme.UseTheme<T>()`** or **`ToTemplateTheme()`** the same way you would use **`CustomConsoleTheme.UseTheme<T>()`** for a **`ConsoleTheme`**.

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
