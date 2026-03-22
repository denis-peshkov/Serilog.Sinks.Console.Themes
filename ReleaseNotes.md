# Release notes

[GitHub Releases](https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/releases)

---

## Unreleased

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

## 1.1.0

- Naming: preset themes exposed as **`CustomConsoleTheme.Dark`** / **`Light`** and **`ConsoleThemes.Dark`** / **`Light`** (aligned configuration strings **`::Dark`** / **`::Light`**).

---

## 1.0.0

- Initial version.
