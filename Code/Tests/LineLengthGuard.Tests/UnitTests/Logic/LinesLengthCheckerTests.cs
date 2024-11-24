using FluentAssertions;
using LineLengthGuard.Logic;
using LineLengthGuard.Settings;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LineLengthGuard.Tests.UnitTests.Logic
{
    [TestClass]
    public class LinesLengthCheckerTests
    {
        [DataTestMethod]
        [DataRow("a")]
        [DataRow("aaaaa")]
        [DataRow("aaaaabbbb")]
        [DataRow("aaaaabbbbb")]
        public void HasAllowedLineLength_ShorterThanMaximumLengthLine_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(new FileSettings { MaximumLineLength = 10 });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aaaaabbbbbc")]
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccddddd")]
        public void HasAllowedLineLength_LongerThanMaximumLengthLine_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(new FileSettings { MaximumLineLength = 10 });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

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
        public void HasAllowedLineLength_ExcludedStart_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    ExcludedLineStarts = [line[0..1]],
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aaaaabbbbbc")]
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccddddd")]
        public void HasAllowedLineLength_NotExcludedStart_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    ExcludedLineStarts = [],
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("A_B_C()")]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("Aaaaa_Bbbbb_Ccccc()")]
        public void HasAllowedLineLength_AllowedMethodNameWithUnderscores_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("Aaaaa_Bbbbb_Ccccc()")]
        public void HasAllowedLineLength_NotAllowedMethodNameWithUnderscores_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = false,
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("\"Aaaaa\";")]
        [DataRow("\"aaaaabbbbb\";")]
        [DataRow("\"aaaaabbbbbccccc\";")]
        public void HasAllowedLineLength_AllowLongStringDefinitions_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = true,
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("\"aaaaabbbbb\";")]
        [DataRow("\"aaaaabbbbbccccc\";")]
        public void HasAllowedLineLength_NotAllowLongStringDefinitions_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = false,
                    MaximumLineLength = 10,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        private static TextLine GetTextLine(string text)
        {
            return SourceText.From(text).Lines.Single();
        }

        private static LinesLengthChecker GetLinesLengthChecker(ISettings settings)
        {
            return new LinesLengthChecker(
                settings,
                new MethodNamesChecker(settings),
                new StringDefinitionsChecker(settings));
        }
    }
}