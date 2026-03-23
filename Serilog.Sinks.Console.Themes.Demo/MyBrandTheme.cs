namespace Serilog.Sinks.Console.Themes.Demo;

/// <summary>Sample custom template: extends <see cref="DarkTheme"/> and overrides a few styles with <see cref="ThemeStyle"/>.</summary>
public sealed class MyBrandTheme : DarkTheme
{
    protected override string Text
        => ThemeStyle.Foreground(Color.Magenta).Italic();

    protected override string LevelDebug
        => ThemeStyle.Background(Color.CadetBlue).FormatType(FormatTypeEnum.BoldMode);

    protected override string Name
        => ThemeStyle.Style(Color.Salmon, Color.Azure);

    protected override string LevelError
        => ThemeStyle.Bold().Italic().Strikethrough().Foreground(Color.Red);
}
