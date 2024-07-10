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
        public void Check_LineShorterThanMaximumLengthAndNotExcludedStart_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = new AllowedLineLengthChecker(
                new FileSettings
                {
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
        public void Check_LineLongerThanMaximumLengthAndNotExcludedStart_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = new AllowedLineLengthChecker(
                new FileSettings
                {
                    MaximumLineLength = 10,
                });

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
        public void Check_LineShorterOrLongerThanMaximumLengthAndExcludedStart_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            AllowedLineLengthChecker allowedLineLengthChecker = new AllowedLineLengthChecker(
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

        private static TextLine GetTextLine(string text)
        {
            return SourceText.From(text).Lines.Single();
        }
    }
}