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
        private const int MaximumLineLength = 10;

        private readonly AllowedLineLengthChecker allowedLineLengthChecker = new AllowedLineLengthChecker(
            new FileSettings
            {
                MaximumLineLength = MaximumLineLength,
            });

        [DataTestMethod]
        [DataRow("a")]
        [DataRow("aaaaa")]
        [DataRow("aaaaabbbb")]
        public void Check_LineShorterThanMaximumLength_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            // Act.
            (bool isAllowed, int lineLength) = this.allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [TestMethod]
        public void Check_LineEqualToMaximumLength_ReturnsTrue()
        {
            // Arrange.
            TextLine textLine = GetTextLine("aaaaabbbbb");

            // Act.
            (bool isAllowed, int lineLength) = this.allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(MaximumLineLength);
        }

        [DataTestMethod]
        [DataRow("aaaaabbbbbc")]
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccddddd")]
        public void Check_LineLongerThanMaximumLength_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            // Act.
            (bool isAllowed, int lineLength) = this.allowedLineLengthChecker.Check(textLine);

            // Assert.
            isAllowed.Should().BeFalse();

            lineLength.Should().Be(line.Length);
        }

        private static TextLine GetTextLine(string text)
        {
            return SourceText.From(text).Lines.Single();
        }
    }
}