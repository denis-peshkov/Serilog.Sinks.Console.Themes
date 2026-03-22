// ReSharper disable CheckNamespace
namespace Serilog.Sinks.Console.Themes;

/// <summary>
/// Same palettes as <see cref="ConsoleThemes"/>, as <see cref="TemplateTheme"/> for use with
/// <see cref="Serilog.Templates.ExpressionTemplate"/> and <c>WriteTo.Console(ITextFormatter)</c>.
/// </summary>
public static class TemplateThemes
{
    private static readonly TemplateTheme DarkInstance = Create<DarkTheme>();

    private static readonly TemplateTheme LightInstance = Create<LightTheme>();

    /// <summary>Dark palette (<see cref="DarkTheme"/>).</summary>
    public static TemplateTheme Dark => DarkInstance;

    /// <summary>Light palette (<see cref="LightTheme"/>).</summary>
    public static TemplateTheme Light => LightInstance;

    /// <summary>Builds a <see cref="TemplateTheme"/> from a <see cref="BaseTheme"/> template.</summary>
    public static TemplateTheme UseTheme<T>() where T : BaseTheme, new() => Create<T>();

    private static TemplateTheme Create<T>() where T : BaseTheme, new() =>
        new T().ToTemplateTheme();
}
