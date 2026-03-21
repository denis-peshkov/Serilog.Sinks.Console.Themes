// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>Default dark-background theme; uses <see cref="CustomConsoleTheme.DarkColors"/>.</summary>
public class DarkTheme : BaseTheme
{
    protected override string Text => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Text);

    protected override string SecondaryText => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.SecondaryText);

    protected override string TertiaryText => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.TertiaryText);

    protected override string Invalid => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Invalid);

    protected override string Null => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Null);

    protected override string Name => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Name);

    protected override string String => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.String);

    protected override string Number => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Number);

    protected override string Boolean => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Boolean);

    protected override string Scalar => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.Scalar);

    protected override string LevelVerbose => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.LevelVerbose);

    protected override string LevelDebug => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.LevelDebug);

    protected override string LevelInformation => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.LevelInformation);

    protected override string LevelWarning => ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.LevelWarning);

    protected override string LevelError =>
        ThemeStyle.Foreground(CustomConsoleTheme.DarkColors.LevelErrorForeground)
            .Background(CustomConsoleTheme.DarkColors.LevelErrorBackground);

    protected override string LevelFatal => TrueColor.BoldForegroundBackground(
        CustomConsoleTheme.DarkColors.LevelFatalForeground,
        CustomConsoleTheme.DarkColors.LevelFatalBackground);
}
