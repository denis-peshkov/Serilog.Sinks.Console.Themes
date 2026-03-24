namespace Serilog.Sinks.Console.Themes.UnitTests;

[TestFixture]
internal sealed class TrueColorConverterTests
{
    [Test]
    public void Foreground_KnownColor_emits_true_color_sgr()
    {
        var c = Color.FromKnownColor(KnownColor.GhostWhite);
        var expected = $"\x1b[38;2;{c.R};{c.G};{c.B}m";
        TrueColorConverter.Foreground(KnownColor.GhostWhite).Should().Be(expected);
    }

    [Test]
    public void Background_KnownColor_emits_true_color_sgr()
    {
        var c = Color.FromKnownColor(KnownColor.Maroon);
        var expected = $"\x1b[48;2;{c.R};{c.G};{c.B}m";
        TrueColorConverter.Background(KnownColor.Maroon).Should().Be(expected);
    }

    [Test]
    public void Foreground_ConsoleColor_matches_Color_FromName()
    {
        var cc = ConsoleColor.DarkYellow;
        var c = Color.FromName(cc.ToString());
        var expected = $"\x1b[38;2;{c.R};{c.G};{c.B}m";
        TrueColorConverter.Foreground(cc).Should().Be(expected);
    }

    [Test]
    public void Background_ConsoleColor_matches_Color_FromName()
    {
        var cc = ConsoleColor.DarkGreen;
        var c = Color.FromName(cc.ToString());
        var expected = $"\x1b[48;2;{c.R};{c.G};{c.B}m";
        TrueColorConverter.Background(cc).Should().Be(expected);
    }

    [Test]
    public void Foreground_Color_and_Background_Color_emit_true_color_sgr()
    {
        var c = Color.FromArgb(11, 22, 33);
        TrueColorConverter.Foreground(c).Should().Be($"\x1b[38;2;{c.R};{c.G};{c.B}m");
        TrueColorConverter.Background(c).Should().Be($"\x1b[48;2;{c.R};{c.G};{c.B}m");
    }

    [Test]
    public void BoldForegroundBackground_KnownColor_prefixes_bold_and_combines_fg_bg()
    {
        var fg = Color.FromKnownColor(KnownColor.White);
        var bg = Color.FromKnownColor(KnownColor.Red);
        var expected = "\x1b[1m"
            + $"\x1b[38;2;{fg.R};{fg.G};{fg.B}m"
            + $"\x1b[48;2;{bg.R};{bg.G};{bg.B}m";
        TrueColorConverter.BoldForegroundBackground(KnownColor.White, KnownColor.Red).Should().Be(expected);
    }

    [Test]
    public void BoldForegroundBackground_ConsoleColor_matches_known_mapping()
    {
        var fg = Color.FromName(ConsoleColor.White.ToString());
        var bg = Color.FromName(ConsoleColor.DarkRed.ToString());
        var expected = "\x1b[1m"
            + $"\x1b[38;2;{fg.R};{fg.G};{fg.B}m"
            + $"\x1b[48;2;{bg.R};{bg.G};{bg.B}m";
        TrueColorConverter.BoldForegroundBackground(ConsoleColor.White, ConsoleColor.DarkRed).Should().Be(expected);
    }

    [Test]
    public void Foreground_system_KnownColor_throws()
    {
        var act = () => TrueColorConverter.Foreground(KnownColor.ActiveCaption);
        act.Should().Throw<ArgumentException>()
            .WithParameterName("known");
    }

    [Test]
    public void Background_system_KnownColor_throws()
    {
        var act = () => TrueColorConverter.Background(KnownColor.Desktop);
        act.Should().Throw<ArgumentException>()
            .WithParameterName("known");
    }
}
