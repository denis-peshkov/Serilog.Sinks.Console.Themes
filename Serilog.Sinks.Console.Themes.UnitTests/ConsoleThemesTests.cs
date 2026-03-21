namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class ConsoleThemesTests
{
    [Test]
    public void Dark_and_Light_are_not_null()
    {
        ConsoleThemes.Dark.Should().NotBeNull();
        ConsoleThemes.Light.Should().NotBeNull();
    }

    [Test]
    public void UseTheme_Dark_matches_Cached_Dark_per_style()
    {
        var cached = ThemeTestHelpers.GetStyles(ConsoleThemes.Dark);
        var fresh = ThemeTestHelpers.GetStyles(ConsoleThemes.UseTheme<DarkTheme>());
        cached.Keys.Should().BeEquivalentTo(fresh.Keys);
        foreach (var key in cached.Keys)
            cached[key].Should().Be(fresh[key]);
    }

    [Test]
    public void UseTheme_Light_matches_Cached_Light_per_style()
    {
        var cached = ThemeTestHelpers.GetStyles(ConsoleThemes.Light);
        var fresh = ThemeTestHelpers.GetStyles(ConsoleThemes.UseTheme<LightTheme>());
        cached.Keys.Should().BeEquivalentTo(fresh.Keys);
        foreach (var key in cached.Keys)
            cached[key].Should().Be(fresh[key]);
    }

    [Test]
    public void UseTheme_custom_subclass_changes_overridden_style_only()
    {
        var theme = ConsoleThemes.UseTheme<TextOverrideTheme>();
        var dict = ThemeTestHelpers.GetStyles(theme);
        dict[ConsoleThemeStyle.Text].Should().Be(TrueColor.Foreground(KnownColor.Gold));
        dict[ConsoleThemeStyle.LevelFatal].Should().Be(
            ThemeTestHelpers.GetStyle(ConsoleThemes.Dark, ConsoleThemeStyle.LevelFatal));
    }

    /// <summary>Minimal override to exercise <see cref="ConsoleThemes.UseTheme{T}"/> with a user-defined <see cref="BaseTheme"/>.</summary>
    private sealed class TextOverrideTheme : DarkTheme
    {
        protected override string Text => TrueColor.Foreground(KnownColor.Gold);
    }
}
