namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class TemplateThemesTests
{
    [Test]
    public void Dark_and_Light_are_not_null()
    {
        TemplateThemes.Dark.Should().NotBeNull();
        TemplateThemes.Light.Should().NotBeNull();
    }

    [Test]
    public void UseTheme_Dark_matches_Cached_Dark_template_output()
    {
        FormatMessage(TemplateThemes.Dark).Should().Be(FormatMessage(TemplateThemes.UseTheme<DarkTheme>()));
    }

    [Test]
    public void UseTheme_Light_matches_Cached_Light_template_output()
    {
        FormatMessage(TemplateThemes.Light).Should().Be(FormatMessage(TemplateThemes.UseTheme<LightTheme>()));
    }

    [Test]
    public void UseTheme_custom_subclass_applies_overridden_text_style_to_message()
    {
        var gold = TrueColor.Foreground(KnownColor.Gold);
        FormatMessage(TemplateThemes.UseTheme<TemplateTextOverrideTheme>()).Should().Contain(gold);
        FormatMessage(TemplateThemes.Dark).Should().NotContain(gold);
    }

    [Test]
    public void ExpressionTemplate_TryParse_succeeds_with_TemplateThemes_Dark()
    {
        var ok = ExpressionTemplate.TryParse(
            "{@l}",
            formatProvider: null,
            nameResolver: null,
            theme: TemplateThemes.Dark,
            applyThemeWhenOutputIsRedirected: false,
            encoder: null,
            result: out var et,
            error: out var err);
        ok.Should().BeTrue();
        err.Should().BeNullOrEmpty();
        et.Should().NotBeNull();
    }

    private static string FormatMessage(TemplateTheme theme)
    {
        var ok = ExpressionTemplate.TryParse(
            "{@m}",
            formatProvider: null,
            nameResolver: null,
            theme: theme,
            applyThemeWhenOutputIsRedirected: true,
            encoder: null,
            result: out var et,
            error: out var err);
        ok.Should().BeTrue();
        err.Should().BeNullOrEmpty();

        var messageTemplate = new MessageTemplateParser().Parse("The message");
        var evt = new LogEvent(
            DateTimeOffset.UtcNow,
            LogEventLevel.Information,
            null,
            messageTemplate,
            Array.Empty<LogEventProperty>());
        var writer = new StringWriter();
        et!.Format(evt, writer);
        return writer.ToString();
    }

    private sealed class TemplateTextOverrideTheme : DarkTheme
    {
        protected override string Text => TrueColor.Foreground(KnownColor.Gold);
    }
}
