# Release Notes - Serilog.Sinks.Console.Themes
https://github.com/denis-peshkov/Serilog.Sinks.Console.Themes/releases

---

## Unreleased - 22 Mar 2024

### Theming API

- **`BaseTheme`** — abstract template: one virtual string per `ConsoleThemeStyle`, exposed via **`ToStyleDictionary()`** for building an `AnsiConsoleTheme`.
- **`DarkTheme`** / **`LightTheme`** — concrete templates driven by **`CustomConsoleTheme.DarkColors`** / **`LightColors`** and **`ThemeStyle`** / **`TrueColor`** (including error/fatal foreground+background and bold fatal).
- **`ConsoleThemes`** — **`Dark`** and **`Light`** singleton themes; **`UseTheme<T>()`** where `T : BaseTheme, new()` for custom templates.
- **`CustomConsoleTheme.DarkTheme`** / **`LightTheme`** — convenience aliases aligned with the same palettes as **`ConsoleThemes.Dark`** / **`Light`**.
- **`ThemeStyle`** — fluent helpers for foreground/background (`KnownColor`, `ConsoleColor`, `Color`), combined **`Style(...)`**, and SGR helpers (**`Bold()`**, **`Italic()`**, **`Underline()`**, **`Strikethrough()`**, **`FormatType(FormatTypeEnum)`**).
- **`FormatTypeEnum`** — named SGR modes (bold, dim, italic, underline, blink, inverse, hidden, strikethrough) plus extension **`string.FormatType(...)`** for chaining on theme fragments.
- **`TrueColor`** — **`Foreground(Color)`** / **`Background(Color)`** for arbitrary RGB; existing **`KnownColor`** / **`ConsoleColor`** mapping; **`BoldForegroundBackground`** overloads; rejects non-portable system **`KnownColor`** values.

### Sample and tests

- **`Serilog.Sinks.Console.Themes.Demo`** — run with **`dark`**, **`light`**, **`custom`**, **`sixteen`**, or **`code`**; **`custom`** uses **`ConsoleThemes.UseTheme<MyTheme>()`** with sample **`MyTheme`** subclassing **`DarkTheme`**; **`sixteen`** / **`code`** use Serilog’s built-in **`AnsiConsoleTheme.Sixteen`** / **`AnsiConsoleTheme.Code`**; no token after **`--`** or an unrecognized token uses **`ConsoleTheme.None`** (no ANSI theme).
- **`Serilog.Sinks.Console.Themes.UnitTests`** — coverage for **`ConsoleThemes`**, **`ThemeStyle`** / **`FormatTypeEnum`**, **`TrueColor`**, **`CustomConsoleTheme`**, shared **`ThemeTestHelpers`**; assembly exposed via **`InternalsVisibleTo`**.

### Documentation and packaging

- **README** — extended quick start (`ConsoleThemes.Dark` / aliases), **`appsettings.json`** (`CustomConsoleTheme::DarkTheme` / **`LightTheme`**, **`ConsoleThemes::Dark`** / **`Light`**, pattern for **`UseTheme<T>()`** via a **static property** in the host app — not a method call in JSON), custom theme examples, screenshots (absolute **`raw.githubusercontent.com`** URLs for NuGet), contributing notes (**`slnx`**, **`dotnet test`**, UTF-8 BOM for `.cs`/`.csproj`, no BOM for **`.slnx`**).
- **`config.nuspec`** — updated **description** / **releaseNotes**; **`img-dark.png`** / **`img-light.png`** included under package **`docs/`** with README.

---

## v1.0.0 - 21 Mar 2024

- Initial version
