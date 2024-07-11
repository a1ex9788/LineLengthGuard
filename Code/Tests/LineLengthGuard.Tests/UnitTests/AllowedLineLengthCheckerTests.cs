using FluentAssertions;
using LineLengthGuard.Settings;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LineLengthGuard.Tests.UnitTests
{
    [TestClass]
    public class AllowedLineLengthCheckerTests
    {
        [DataTestMethod]
        [DataRow("a")]
        [DataRow("aaaaa")]
        [DataRow("aaaaabbbb")]
        [DataRow("aaaaabbbbb")]
        public void Check_ShorterThanMaximumLengthLine_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = GetAllowedLineLengthChecker(
                new FileSettings { MaximumLineLength = 10 });

            // Act.
            (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aaaaabbbbbc")]
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccddddd")]
        public void Check_LongerThanMaximumLengthLine_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = GetAllowedLineLengthChecker(
                new FileSettings { MaximumLineLength = 10 });

            // Act.
            (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aa")]
        [DataRow("aaaaa")]
        [DataRow("aaaaabbbb")]
        [DataRow("aaaaabbbbb")]
        [DataRow("aaaaabbbbbc")]
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccddddd")]
        public void Check_ExcludedStart_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = GetAllowedLineLengthChecker(
                new FileSettings
                {
                    ExcludedLineStarts = [line[0..1]],
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aaaaabbbbbc")]
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccddddd")]
        public void Check_NotExcludedStart_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = GetAllowedLineLengthChecker(
                new FileSettings
                {
                    ExcludedLineStarts = [],
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("A_B_C()")]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("Aaaaa_Bbbbb_Ccccc()")]
        public void Check_AllowedMethodNameWithUnderscores_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = GetAllowedLineLengthChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("Aaaaa_Bbbbb_Ccccc()")]
        public void Check_NotAllowedMethodNameWithUnderscores_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = GetAllowedLineLengthChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = false,
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        private static TextLine GetTextLine(string text)
        {
            return SourceText.From(text).Lines.Single();
        }

        private static AllowedLineLengthChecker GetAllowedLineLengthChecker(ISettings settings)
        {
            return new AllowedLineLengthChecker(settings, new MethodNamesChecker(settings));
        }
    }
}