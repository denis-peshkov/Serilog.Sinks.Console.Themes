namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class ThemeStyleTests
{
    [Test]
    public void Unthemed_is_empty() =>
        ThemeStyle.Unthemed.Should().BeEmpty();

    [Test]
    public void Static_Foreground_KnownColor_delegates_to_TrueColorConverter()
    {
        var expected = TrueColorConverter.Foreground(KnownColor.Coral);
        ThemeStyle.Foreground(KnownColor.Coral).Should().Be(expected);
    }

    [Test]
    public void Static_Foreground_ConsoleColor_delegates_to_TrueColorConverter()
    {
        var expected = TrueColorConverter.Foreground(ConsoleColor.DarkCyan);
        ThemeStyle.Foreground(ConsoleColor.DarkCyan).Should().Be(expected);
    }

    [Test]
    public void Static_Foreground_Color_delegates_to_TrueColorConverter()
    {
        var color = Color.FromArgb(10, 20, 30);
        ThemeStyle.Foreground(color).Should().Be(TrueColorConverter.Foreground(color));
    }

    [Test]
    public void Static_Background_overloads_delegate_to_TrueColorConverter()
    {
        ThemeStyle.Background(KnownColor.Azure).Should().Be(TrueColorConverter.Background(KnownColor.Azure));
        ThemeStyle.Background(ConsoleColor.Magenta).Should().Be(TrueColorConverter.Background(ConsoleColor.Magenta));
        var rgb = Color.FromArgb(40, 50, 60);
        ThemeStyle.Background(rgb).Should().Be(TrueColorConverter.Background(rgb));
    }

    [Test]
    public void Static_FormatType_None_is_empty() =>
        ThemeStyle.FormatType(FormatTypeEnum.None).Should().BeEmpty();

    [TestCase(FormatTypeEnum.BoldMode, "1")]
    [TestCase(FormatTypeEnum.DimFaintMode, "2")]
    [TestCase(FormatTypeEnum.ItalicMode, "3")]
    [TestCase(FormatTypeEnum.UnderlineMode, "4")]
    [TestCase(FormatTypeEnum.BlinkingMode, "5")]
    [TestCase(FormatTypeEnum.InverseReverseMode, "6")]
    [TestCase(FormatTypeEnum.HiddenMode, "7")]
    [TestCase(FormatTypeEnum.Strikethrough, "8")]
    public void Static_FormatType_emits_sgr(FormatTypeEnum formatType, string code) =>
        ThemeStyle.FormatType(formatType).Should().Be($"\x1b[{code}m");

    [Test]
    public void Static_FormatType_unknown_enum_value_uses_default_sgr_reset() =>
        ThemeStyle.FormatType((FormatTypeEnum)999).Should().Be("\x1b[0m");

    [Test]
    public void Static_Bold_Italic_Underline_Strikethrough_match_FormatType()
    {
        ThemeStyle.Bold().Should().Be(ThemeStyle.FormatType(FormatTypeEnum.BoldMode));
        ThemeStyle.Italic().Should().Be(ThemeStyle.FormatType(FormatTypeEnum.ItalicMode));
        ThemeStyle.Underline().Should().Be(ThemeStyle.FormatType(FormatTypeEnum.UnderlineMode));
        ThemeStyle.Strikethrough().Should().Be(ThemeStyle.FormatType(FormatTypeEnum.Strikethrough));
    }

    [Test]
    public void Extension_FormatType_None_returns_original_string()
    {
        const string prefix = "\x1b[31m";
        prefix.FormatType(FormatTypeEnum.None).Should().Be(prefix);
    }

    [Test]
    public void Extension_FormatType_appends_to_existing()
    {
        "\x1b[1m".FormatType(FormatTypeEnum.ItalicMode).Should().Be("\x1b[1m\x1b[3m");
    }

    [Test]
    public void Extension_Foreground_and_Background_append()
    {
        var red = TrueColorConverter.Foreground(KnownColor.Red);
        var blue = TrueColorConverter.Background(KnownColor.Blue);
        string.Empty.Foreground(KnownColor.Red).Should().Be(red);
        string.Empty.Background(KnownColor.Blue).Should().Be(blue);
        "\x1b[1m".Foreground(ConsoleColor.Yellow).Should().Be("\x1b[1m" + TrueColorConverter.Foreground(ConsoleColor.Yellow));
        var c = Color.Plum;
        "\x1b[1m".Foreground(c).Should().Be("\x1b[1m" + TrueColorConverter.Foreground(c));
        "\x1b[1m".Background(KnownColor.Green).Should().Be("\x1b[1m" + TrueColorConverter.Background(KnownColor.Green));
        "\x1b[1m".Background(ConsoleColor.DarkBlue).Should().Be("\x1b[1m" + TrueColorConverter.Background(ConsoleColor.DarkBlue));
        "\x1b[1m".Background(c).Should().Be("\x1b[1m" + TrueColorConverter.Background(c));
    }

    [Test]
    public void Extension_Bold_Italic_Underline_Strikethrough_chain()
    {
        string.Empty.Bold().Should().Be(ThemeStyle.Bold());
        string.Empty.Italic().Should().Be(ThemeStyle.Italic());
        string.Empty.Underline().Should().Be(ThemeStyle.Underline());
        string.Empty.Strikethrough().Should().Be(ThemeStyle.Strikethrough());
    }

    [Test]
    public void Style_Color_pair_and_FormatType_overloads()
    {
        var fg = Color.Salmon;
        var bg = Color.Azure;
        ThemeStyle.Style(fg).Should().Be(TrueColorConverter.Foreground(fg));
        ThemeStyle.Style(fg, FormatTypeEnum.BoldMode)
            .Should().Be(TrueColorConverter.Foreground(fg) + "\x1b[1m");
        ThemeStyle.Style(fg, bg, FormatTypeEnum.ItalicMode)
            .Should().Be(TrueColorConverter.Foreground(fg) + TrueColorConverter.Background(bg) + "\x1b[3m");
        ThemeStyle.Style(fg, bg).Should().Be(TrueColorConverter.Foreground(fg) + TrueColorConverter.Background(bg));
    }

    [Test]
    public void ToSgrParameter_None_branch_is_reachable_via_reflection()
    {
        var method = typeof(ThemeStyle).GetMethod(
            "ToSgrParameter",
            BindingFlags.NonPublic | BindingFlags.Static,
            null,
            new[] { typeof(FormatTypeEnum) },
            null);
        method.Should().NotBeNull();
        var result = (string)method!.Invoke(null, new object[] { FormatTypeEnum.None })!;
        result.Should().Be("0");
    }
}
