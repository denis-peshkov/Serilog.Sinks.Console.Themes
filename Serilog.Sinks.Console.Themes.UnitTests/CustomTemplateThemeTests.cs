namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class CustomTemplateThemeTests
{
    [Test]
    public void Dark_and_Light_are_not_null()
    {
        CustomTemplateTheme.Dark.Should().NotBeNull();
        CustomTemplateTheme.Light.Should().NotBeNull();
    }

    [Test]
    public void UseTheme_Dark_matches_Cached_Dark_template_output()
    {
        FormatMessage(CustomTemplateTheme.Dark).Should().Be(FormatMessage(CustomTemplateTheme.UseTheme<DarkTheme>()));
    }

    [Test]
    public void UseTheme_Light_matches_Cached_Light_template_output()
    {
        FormatMessage(CustomTemplateTheme.Light).Should().Be(FormatMessage(CustomTemplateTheme.UseTheme<LightTheme>()));
    }

    [Test]
    public void UseTheme_custom_subclass_applies_overridden_text_style_to_message()
    {
        var gold = TrueColorConverter.Foreground(KnownColor.Gold);
        FormatMessage(CustomTemplateTheme.UseTheme<TemplateTextOverrideTheme>()).Should().Contain(gold);
        FormatMessage(CustomTemplateTheme.Dark).Should().NotContain(gold);
    }

    [Test]
    public void ExpressionTemplate_TryParse_succeeds_with_CustomTemplateTheme_Dark()
    {
        var ok = ExpressionTemplate.TryParse(
            "{@l}",
            formatProvider: null,
            nameResolver: null,
            theme: CustomTemplateTheme.Dark,
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
        protected override string Text => TrueColorConverter.Foreground(KnownColor.Gold);
    }
}
