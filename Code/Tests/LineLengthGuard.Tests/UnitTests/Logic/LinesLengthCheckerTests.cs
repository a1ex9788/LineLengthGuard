using FluentAssertions;
using LineLengthGuard.Logic;
using LineLengthGuard.Logic.Utilities;
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
        [DataRow("<see cref=\"ClassReference\"/>")]
        [DataRow("http://server")]
        [DataRow("https://server")]
        public void HasAllowedLineLength_DefaultExcludedLinePattern_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(new FileSettings { MaximumLineLength = 1 });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("<see aaa/>")]
        [DataRow("htt://server")]
        public void HasAllowedLineLength_NotDefaultExcludedLinePattern_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(new FileSettings { MaximumLineLength = 1 });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aa")]
        [DataRow("aaa")]
        [DataRow("aaabb")]
        [DataRow("aaabbb")]
        public void HasAllowedLineLength_ExcludedStart_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    ExcludedLineStarts = [line[0..1]],
                    MaximumLineLength = 1,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("aa")]
        [DataRow("aaa")]
        [DataRow("aaabb")]
        [DataRow("aaabbb")]
        public void HasAllowedLineLength_NotExcludedStart_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    ExcludedLineStarts = [],
                    MaximumLineLength = 1,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("Aaa_Bbb()")]
        [DataRow("Aaa_Bbb_Ccc()")]
        public void HasAllowedLineLength_AllowedMethodNameWithUnderscores_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                    MaximumLineLength = 1,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("Aaa_Bbb()")]
        [DataRow("Aaa_Bbb_Ccc()")]
        public void HasAllowedLineLength_NotAllowedMethodNameWithUnderscores_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = false,
                    MaximumLineLength = 1,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("\"aaa\";")]
        [DataRow("\"aaabbb\";")]
        public void HasAllowedLineLength_AllowLongStringDefinitions_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = true,
                    MaximumLineLength = 1,
                });

            // Act.
            (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [DataTestMethod]
        [DataRow("\"aaa\";")]
        [DataRow("\"aaabbb\";")]
        public void HasAllowedLineLength_NotAllowLongStringDefinitions_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            LinesLengthChecker linesLengthChecker = GetLinesLengthChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = false,
                    MaximumLineLength = 1,
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
                new ReferencesInDocumentationChecker(),
                new StringDefinitionsChecker(settings),
                new URLsChecker());
        }
    }
}