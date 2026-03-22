namespace Serilog.Sinks.Console.Themes;

/// <summary>Maps Serilog console sink style keys (<see cref="ConsoleThemeStyle"/>) to
/// <see cref="ExpressionTemplate"/> style keys (<see cref="TemplateThemeStyle"/>).</summary>
public static class ThemeStyleConverter
{
    /// <summary>
    /// Builds a dictionary keyed by <see cref="TemplateThemeStyle"/> with the same escape sequences as
    /// <paramref name="consoleStyles"/>. Each <see cref="ConsoleThemeStyle"/> member name must match a
    /// <see cref="TemplateThemeStyle"/> member name (numeric enum strings are rejected).
    /// </summary>
    /// <exception cref="ArgumentException">When a <see cref="ConsoleThemeStyle"/> name has no matching <see cref="TemplateThemeStyle"/>.</exception>
    public static Dictionary<TemplateThemeStyle, string> ToTemplateStyles(
        IReadOnlyDictionary<ConsoleThemeStyle, string> consoleStyles)
    {
        var result = new Dictionary<TemplateThemeStyle, string>(consoleStyles.Count);
        foreach (var kv in consoleStyles)
        {
            // Scalar and obsolete Object share an underlying value; ToString() may be "Object" on some runtimes.
            TemplateThemeStyle templateStyle;
            if ((int)kv.Key == (int)ConsoleThemeStyle.Scalar)
                templateStyle = TemplateThemeStyle.Scalar;
            else
            {
                var name = kv.Key.ToString();
                if (!Enum.TryParse(name, ignoreCase: false, out templateStyle)
                    || !Enum.IsDefined(typeof(TemplateThemeStyle), templateStyle))
                    throw new ArgumentException(
                        $"No {nameof(TemplateThemeStyle)} member named '{name}' for console style '{kv.Key}'.",
                        nameof(consoleStyles));
            }

            result[templateStyle] = kv.Value;
        }

        return result;
    }
}
