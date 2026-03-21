namespace Serilog.Sinks.Console.Themes;

/// <summary>Fluent styling for <see cref="BaseTheme"/> templates: foreground, background, and SGR modes.</summary>
public static class ThemeStyle
{
    public static string Unthemed => string.Empty;

    public static string Foreground(KnownColor known) => TrueColor.Foreground(known);

    public static string Foreground(ConsoleColor console) => TrueColor.Foreground(console);

    public static string Foreground(Color color) => TrueColor.Foreground(color);

    public static string Background(KnownColor known) => TrueColor.Background(known);

    public static string Background(ConsoleColor console) => TrueColor.Background(console);

    public static string Background(Color color) => TrueColor.Background(color);

    public static string FormatType(FormatTypeEnum formatType) => string.Empty.FormatType(formatType);

    public static string Bold() => FormatType(FormatTypeEnum.BoldMode);

    public static string Italic() => FormatType(FormatTypeEnum.ItalicMode);

    public static string Underline() => FormatType(FormatTypeEnum.UnderlineMode);

    public static string Strikethrough() => FormatType(FormatTypeEnum.Strikethrough);

    public static string Style(Color foreground, Color background, FormatTypeEnum formatType = FormatTypeEnum.None) => string.Empty.Foreground(foreground).Background(background).FormatType(formatType);

    public static string Style(Color foreground, FormatTypeEnum formatType) => string.Empty.Foreground(foreground).FormatType(formatType);

    public static string Style(Color foreground) => Foreground(foreground);

    public static string Foreground(this string logStyle, KnownColor known) => logStyle + TrueColor.Foreground(known);

    public static string Foreground(this string logStyle, ConsoleColor console) => logStyle + TrueColor.Foreground(console);

    public static string Foreground(this string logStyle, Color color) => logStyle + TrueColor.Foreground(color);

    public static string Background(this string logStyle, KnownColor known) => logStyle + TrueColor.Background(known);

    public static string Background(this string logStyle, ConsoleColor console) => logStyle + TrueColor.Background(console);

    public static string Background(this string logStyle, Color color) => logStyle + TrueColor.Background(color);

    public static string FormatType(this string logStyle, FormatTypeEnum formatType)
    {
        if (formatType == FormatTypeEnum.None)
            return logStyle;
        return logStyle + $"\x1b[{ToSgrParameter(formatType)}m";
    }

    public static string Bold(this string logStyle) => logStyle.FormatType(FormatTypeEnum.BoldMode);

    public static string Italic(this string logStyle) => logStyle.FormatType(FormatTypeEnum.ItalicMode);

    public static string Underline(this string logStyle) => logStyle.FormatType(FormatTypeEnum.UnderlineMode);

    public static string Strikethrough(this string logStyle) => logStyle.FormatType(FormatTypeEnum.Strikethrough);

    private static string ToSgrParameter(FormatTypeEnum formatType) =>
        formatType switch
        {
            FormatTypeEnum.None => "0",
            FormatTypeEnum.BoldMode => "1",
            FormatTypeEnum.DimFaintMode => "2",
            FormatTypeEnum.ItalicMode => "3",
            FormatTypeEnum.UnderlineMode => "4",
            FormatTypeEnum.BlinkingMode => "5",
            FormatTypeEnum.InverseReverseMode => "6",
            FormatTypeEnum.HiddenMode => "7",
            FormatTypeEnum.Strikethrough => "8",
            _ => "0",
        };
}
