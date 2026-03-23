# Release notes

[GitHub Releases](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/releases)

---

## 4.0.0 - 25 Mar 2024

### Breaking change

- Renamed **`TemplateThemes`** → **`CustomTemplateTheme`** (and **`TemplateThemesTests`** → **`CustomTemplateThemeTests`**). Update code and **`appsettings`** references if you used the old type name.
- Removed **`CustomConsoleTheme.DarkTemplateTheme`** / **`LightTemplateTheme`** — use **`CustomTemplateTheme.Dark`** / **`Light`** for **`ExpressionTemplate`**.
- Renamed **`TrueColor`** → **`TrueColorConverter`** (file **`TrueColor.cs`** → **`TrueColorConverter.cs`**; tests **`TrueColorTests`** → **`TrueColorConverterTests`**).

### Theming API

- **`CustomTemplateTheme`** — cached **`Dark`** / **`Light`** as **`TemplateTheme`**, plus **`UseTheme<T>()`** where **`T : BaseTheme, new()`**.
- **`CustomConsoleTheme`** — **`ConsoleTheme`** presets only; **`ExpressionTemplate`** themes are **`CustomTemplateTheme.Dark`** / **`Light`** (no duplicate properties on **`CustomConsoleTheme`**).
- **`BaseTheme`** — **`ToTemplateStyleDictionary()`** and **`ToTemplateTheme()`** to feed **`ExpressionTemplate`** / **`TryParse`**.
- **`ThemeStyleConverter.ToTemplateStyles(...)`** — maps **`IReadOnlyDictionary<ConsoleThemeStyle, string>`** to **`Dictionary<TemplateThemeStyle, string>`** by enum name, with a special case for **`ConsoleThemeStyle.Scalar`** vs obsolete **`Object`**; throws **`ArgumentException`** when a console style name does not match **`TemplateThemeStyle`** (including bogus numeric enum values).
- **`GlobalUsings.cs`** (library) — **`Serilog.Templates`** and **`Serilog.Templates.Themes`**.

### Sample and tests

- **`CustomTemplateThemeTests`** — non-null dark/light, **`UseTheme<DarkTheme/LightTheme>`** matches cached themes, custom **`DarkTheme`** override affects formatted output, **`ExpressionTemplate.TryParse`** with **`CustomTemplateTheme.Dark`**.
- **`ThemeStyleConverterTests`** — full style map from **`DarkTheme`**, parity with **`BaseTheme.ToTemplateStyleDictionary()`**, invalid **`ConsoleThemeStyle`** throws.
- **`CustomConsoleThemeTests`** — console theme caching and **`UseTheme<T>()`**.
- **`ThemeStyleTests`** — covers private **`ToSgrParameter(FormatTypeEnum.None)`** branch (reflection) for full coverage of **`ThemeStyle`**.
- **`TrueColorConverterTests`** — SGR fragments from **`KnownColor`** / **`ConsoleColor`** / **`Color`**, bold combinations, system **`KnownColor`** rejection.
- **`GlobalUsings.cs`** (tests) — **`System.IO`**, **`Serilog.Events`**, **`Serilog.Parsing`**, **`Serilog.Templates`**, **`Serilog.Templates.Themes`**.

### Documentation and packaging

- **README** — **`ExpressionTemplate` and `TemplateTheme`** section: **[Serilog.Expressions](https://www.nuget.org/packages/Serilog.Expressions/)**, **`CustomTemplateTheme`** for presets, sample **`ExpressionTemplate`** + **`WriteTo.Console(formatter)`**, optional **`TryParse`** parameters; **Features** list updated.
- **`Serilog.Expressions` 5.0.0** — direct package reference in the library project and explicit **NuGet** dependency in **`config.nuspec`** (alongside **`Serilog.Sinks.Console`**).
- **`config.nuspec`** — **`releaseNotes`** updated to describe **ExpressionTemplate** / **TemplateTheme** support.

---

## 3.0.0 - 23 Mar 2024

### Breaking change

- Removed **`ConsoleThemes`**. Use **`CustomConsoleTheme.Dark`**, **`CustomConsoleTheme.Light`**, and **`CustomConsoleTheme.UseTheme<T>()`** instead (same behavior; single public entry type).

### Theming API

- **`CustomConsoleTheme`** — cached **`Dark`** / **`Light`**, **`UseTheme<T>()`** for `T : BaseTheme, new()`, and **`DarkColors`** / **`LightColors`** palette constants.
- **`BaseTheme`**, **`DarkTheme`**, **`LightTheme`**, **`ThemeStyle`**, **`TrueColor`** — unchanged roles.

### Documentation

- **README** and **`config.nuspec`** — all samples and strings use **`CustomConsoleTheme`** only.

---

## 2.0.0 - 23 Mar 2024

### Theming API

- **`BaseTheme`** — abstract template: one string per `ConsoleThemeStyle`, materialized via **`ToStyleDictionary()`** into an `AnsiConsoleTheme`.
- **`DarkTheme`** / **`LightTheme`** — concrete templates using **`CustomConsoleTheme.DarkColors`** / **`LightColors`** with **`ThemeStyle`** and **`TrueColor`**.
- **`ConsoleThemes`** — cached **`Dark`** / **`Light`**; **`UseTheme<T>()`** for `T : BaseTheme, new()`.
- **`CustomConsoleTheme.Dark`** / **`Light`** — same instances as **`ConsoleThemes.Dark`** / **`Light`** (handy aliases for `WriteTo.Console` and **`Serilog.Settings.Configuration`** `::Dark` / `::Light`).
- **`ThemeStyle`**, **`FormatTypeEnum`**, **`TrueColor`** — fluent styling and RGB / `KnownColor` / `ConsoleColor` SGR fragments.

### Sample and tests

- **`Serilog.Sinks.Console.Themes.Demo`** — **`dark`**, **`light`**, **`custom`**, **`sixteen`**, **`code`**, or **`--`** with no token / unknown token (**`ConsoleTheme.None`**).
- **`Serilog.Sinks.Console.Themes.UnitTests`** — themes, **`ThemeStyle`**, **`TrueColor`**, helpers; **`InternalsVisibleTo`** where used.

### Documentation and packaging

- **README** — `appsettings` theme string uses **`CustomConsoleTheme::Dark`** / **`::Light`** (replacing the old **`::DarkTheme`** / **`::LightTheme`** members); screenshots via **`raw.githubusercontent.com`**; contributing / **`slnx`** / encoding notes.
- **`config.nuspec`** — description and packaged **`docs/`** assets (README + screenshots when listed in nuspec).

---

## 1.1.0 - 22 Mar 2024

- Naming: preset themes exposed as **`CustomConsoleTheme.Dark`** / **`Light`** and **`ConsoleThemes.Dark`** / **`Light`** (aligned configuration strings **`::Dark`** / **`::Light`**).

---

## 1.0.0 - 21 Mar 2024

- Initial version.
