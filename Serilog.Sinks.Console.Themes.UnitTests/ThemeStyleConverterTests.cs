namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class ThemeStyleConverterTests
{
    [Test]
    public void ToTemplateStyles_maps_each_console_style_by_name()
    {
        var ansi = new DarkTheme().ToStyleDictionary();
        var template = ThemeStyleConverter.ToTemplateStyles(ansi);
        template.Count.Should().Be(ansi.Count);
        foreach (var kv in ansi)
        {
            var one = new Dictionary<ConsoleThemeStyle, string> { [kv.Key] = kv.Value };
            var roundTrip = ThemeStyleConverter.ToTemplateStyles(one);
            roundTrip.Values.Single().Should().Be(kv.Value);
        }
    }

    [Test]
    public void ToTemplateStyles_matches_BaseTheme_ToTemplateStyleDictionary()
    {
        var dark = new DarkTheme();
        dark.ToTemplateStyleDictionary().Should().BeEquivalentTo(ThemeStyleConverter.ToTemplateStyles(dark.ToStyleDictionary()));
    }

    [Test]
    public void ToTemplateStyles_unknown_console_enum_value_throws()
    {
        // Numeric string from ToString() must not bypass mapping via Enum.TryParse alone.
        var bad = new Dictionary<ConsoleThemeStyle, string>
        {
            [(ConsoleThemeStyle)ushort.MaxValue] = "\x1b[0m",
        };
        var act = () => ThemeStyleConverter.ToTemplateStyles(bad);
        act.Should().Throw<ArgumentException>().WithParameterName("consoleStyles");
    }
}
