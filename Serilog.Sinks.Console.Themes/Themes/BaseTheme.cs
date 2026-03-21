// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>
/// Base type for console theme templates. Override any style, then use
/// <see cref="ConsoleThemes.UseTheme{T}"/> to build a <see cref="ConsoleTheme"/> for <c>WriteTo.Console</c>.
/// </summary>
public abstract class BaseTheme
{
    protected abstract string Text { get; }

    protected abstract string SecondaryText { get; }

    protected abstract string TertiaryText { get; }

    protected abstract string Invalid { get; }

    protected abstract string Null { get; }

    protected abstract string Name { get; }

    protected abstract string String { get; }

    protected abstract string Number { get; }

    protected abstract string Boolean { get; }

    protected abstract string Scalar { get; }

    protected abstract string LevelVerbose { get; }

    protected abstract string LevelDebug { get; }

    protected abstract string LevelInformation { get; }

    protected abstract string LevelWarning { get; }

    protected abstract string LevelError { get; }

    protected abstract string LevelFatal { get; }

    /// <summary>Maps this template to Serilog <see cref="ConsoleThemeStyle"/> keys.</summary>
    public Dictionary<ConsoleThemeStyle, string> ToStyleDictionary() => new()
    {
        [ConsoleThemeStyle.Text] = Text,
        [ConsoleThemeStyle.SecondaryText] = SecondaryText,
        [ConsoleThemeStyle.TertiaryText] = TertiaryText,
        [ConsoleThemeStyle.Invalid] = Invalid,
        [ConsoleThemeStyle.Null] = Null,
        [ConsoleThemeStyle.Name] = Name,
        [ConsoleThemeStyle.String] = String,
        [ConsoleThemeStyle.Number] = Number,
        [ConsoleThemeStyle.Boolean] = Boolean,
        [ConsoleThemeStyle.Scalar] = Scalar,
        [ConsoleThemeStyle.LevelVerbose] = LevelVerbose,
        [ConsoleThemeStyle.LevelDebug] = LevelDebug,
        [ConsoleThemeStyle.LevelInformation] = LevelInformation,
        [ConsoleThemeStyle.LevelWarning] = LevelWarning,
        [ConsoleThemeStyle.LevelError] = LevelError,
        [ConsoleThemeStyle.LevelFatal] = LevelFatal,
    };
}
