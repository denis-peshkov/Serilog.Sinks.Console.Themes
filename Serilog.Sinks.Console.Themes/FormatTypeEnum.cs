namespace Serilog.Sinks.Console.Themes;

/// <summary>SGR display attributes combined with foreground/background sequences from <see cref="ThemeStyle"/>.</summary>
public enum FormatTypeEnum
{
    None = 0,
    BoldMode = 1,
    DimFaintMode = 2,
    ItalicMode = 3,
    UnderlineMode = 4,
    BlinkingMode = 5,
    InverseReverseMode = 6,
    HiddenMode = 7,
    Strikethrough = 8,
}
