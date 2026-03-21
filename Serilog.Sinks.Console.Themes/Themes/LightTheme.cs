// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>Light-background theme; uses <see cref="CustomConsoleTheme.LightColors"/>.</summary>
public class LightTheme : BaseTheme
{
    protected override string Text => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Text);

    protected override string SecondaryText => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.SecondaryText);

    protected override string TertiaryText => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.TertiaryText);

    protected override string Invalid => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Invalid);

    protected override string Null => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Null);

    protected override string Name => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Name);

    protected override string String => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.String);

    protected override string Number => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Number);

    protected override string Boolean => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Boolean);

    protected override string Scalar => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.Scalar);

    protected override string LevelVerbose => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.LevelVerbose);

    protected override string LevelDebug => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.LevelDebug);

    protected override string LevelInformation => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.LevelInformation);

    protected override string LevelWarning => ThemeStyle.Foreground(CustomConsoleTheme.LightColors.LevelWarning);

    protected override string LevelError =>
        ThemeStyle.Foreground(CustomConsoleTheme.LightColors.LevelErrorForeground)
            .Background(CustomConsoleTheme.LightColors.LevelErrorBackground);

    protected override string LevelFatal => TrueColor.BoldForegroundBackground(
        CustomConsoleTheme.LightColors.LevelFatalForeground,
        CustomConsoleTheme.LightColors.LevelFatalBackground);
}
