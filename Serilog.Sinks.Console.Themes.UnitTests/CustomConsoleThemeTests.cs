namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class CustomConsoleThemeTests
{
    [Test]
    public void Dark_and_Light_are_console_themes_with_full_style_map()
    {
        CustomConsoleTheme.Dark.Should().BeAssignableTo<ConsoleTheme>();
        CustomConsoleTheme.Light.Should().BeAssignableTo<ConsoleTheme>();

        // ConsoleThemeStyle.Object is obsolete and shares its underlying value with Scalar, so the theme dictionary has one entry per distinct value.
        var expected = Enum.GetValues<ConsoleThemeStyle>().Cast<int>().Distinct().Count();
        ThemeTestHelpers.GetStyleCount(CustomConsoleTheme.Dark).Should().Be(expected);
        ThemeTestHelpers.GetStyleCount(CustomConsoleTheme.Light).Should().Be(expected);
    }

    [Test]
    public void Dark_and_Light_define_distinct_level_information_colors()
    {
        var dark = ThemeTestHelpers.GetStyle(CustomConsoleTheme.Dark, ConsoleThemeStyle.LevelInformation);
        var light = ThemeTestHelpers.GetStyle(CustomConsoleTheme.Light, ConsoleThemeStyle.LevelInformation);
        dark.Should().NotBe(light);
    }

    [Test]
    public void Dark_and_Light_presets_are_cached_singletons()
    {
        ReferenceEquals(CustomConsoleTheme.Dark, CustomConsoleTheme.Dark).Should().BeTrue();
        ReferenceEquals(CustomConsoleTheme.Light, CustomConsoleTheme.Light).Should().BeTrue();
    }

    [Test]
    public void UseTheme_Dark_matches_Cached_Dark_per_style()
    {
        var cached = ThemeTestHelpers.GetStyles(CustomConsoleTheme.Dark);
        var fresh = ThemeTestHelpers.GetStyles(CustomConsoleTheme.UseTheme<DarkTheme>());
        cached.Keys.Should().BeEquivalentTo(fresh.Keys);
        foreach (var key in cached.Keys)
            cached[key].Should().Be(fresh[key]);
    }

    [Test]
    public void UseTheme_Light_matches_Cached_Light_per_style()
    {
        var cached = ThemeTestHelpers.GetStyles(CustomConsoleTheme.Light);
        var fresh = ThemeTestHelpers.GetStyles(CustomConsoleTheme.UseTheme<LightTheme>());
        cached.Keys.Should().BeEquivalentTo(fresh.Keys);
        foreach (var key in cached.Keys)
            cached[key].Should().Be(fresh[key]);
    }

    [Test]
    public void UseTheme_custom_subclass_changes_overridden_style_only()
    {
        var theme = CustomConsoleTheme.UseTheme<TextOverrideTheme>();
        var dict = ThemeTestHelpers.GetStyles(theme);
        dict[ConsoleThemeStyle.Text].Should().Be(TrueColorConverter.Foreground(KnownColor.Gold));
        dict[ConsoleThemeStyle.LevelFatal].Should().Be(
            ThemeTestHelpers.GetStyle(CustomConsoleTheme.Dark, ConsoleThemeStyle.LevelFatal));
    }

    /// <summary>Minimal override to exercise <see cref="CustomConsoleTheme.UseTheme{T}"/> with a user-defined <see cref="BaseTheme"/>.</summary>
    private sealed class TextOverrideTheme : DarkTheme
    {
        protected override string Text => TrueColorConverter.Foreground(KnownColor.Gold);
    }

}
