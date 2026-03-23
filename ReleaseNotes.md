# Release notes

[GitHub Releases](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/releases)

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
- **`CustomConsoleTheme.Dark`** / **`Light`** — same instances as **`ConsoleThemes`**; **`Serilog.Settings.Configuration`** `CustomConsoleTheme::Dark` / `::Light`.
- **`ThemeStyle`**, **`FormatTypeEnum`**, **`TrueColor`** — fluent styling and RGB / `KnownColor` / `ConsoleColor` SGR fragments.

### Sample and tests

- **`Serilog.Sinks.Console.Themes.Demo`** — **`dark`**, **`light`**, **`custom`**, **`sixteen`**, **`code`**, or **`--`** with no token / unknown token (**`ConsoleTheme.None`**).
- **`Serilog.Sinks.Console.Themes.UnitTests`** — themes, **`ThemeStyle`**, **`TrueColor`**, helpers; **`InternalsVisibleTo`** where used.

### Documentation and packaging

- **README** — built-in `theme` in `appsettings` as **`CustomConsoleTheme::Dark`** / **`::Light`**; screenshots via **`raw.githubusercontent.com`**; contributing / **`slnx`** / encoding notes.
- **`config.nuspec`** — description and packaged **`docs/`** assets (README + screenshots when listed in nuspec).

---

## 1.1.0 - 22 Mar 2024

- Naming: preset themes exposed as **`CustomConsoleTheme.Dark`** / **`Light`** and duplicated on **`ConsoleThemes`** (same instances); configuration **`CustomConsoleTheme::Dark`** / **`::Light`**. (**`ConsoleThemes`** removed in **3.0.0**.)

---

## 1.0.0 - 21 Mar 2024

- Initial version.
