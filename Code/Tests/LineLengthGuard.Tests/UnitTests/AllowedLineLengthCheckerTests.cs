using FluentAssertions;
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
        [DataRow("aaaaabbbbbccccc")]
        [DataRow("aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiii")]
        [DataRow("aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrrsssss")]
        public void IsLineLengthAllowed_LineShorterThanMaximumLength_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            // Act.
            (bool isAllowed, int lineLength) = AllowedLineLengthChecker.IsLineLengthAllowed(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(line.Length);
        }

        [TestMethod]
        public void IsLineLengthAllowed_LineEqualToMaximumLength_ReturnsTrue()
        {
            // Arrange.
            TextLine textLine = GetTextLine("""
aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrrssssstttttuuuuuvvvvvwwwwwxxxxx
""");

            // Act.
            (bool isAllowed, int lineLength) = AllowedLineLengthChecker.IsLineLengthAllowed(textLine);

            // Assert.
            isAllowed.Should().BeTrue();

            lineLength.Should().Be(120);
        }

        [DataTestMethod]
        [DataRow("""
aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrrssssstttttuuuuuvvvvvwwwwwxxxxxy
""")]
        [DataRow("""
aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrrssssstttttuuuuuvvvvvwwwwwxxxxxyyyyy
""")]
        public void IsLineLengthAllowed_LineLongerThanMaximumLength_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            TextLine textLine = GetTextLine(line);

            // Act.
            (bool isAllowed, int lineLength) = AllowedLineLengthChecker.IsLineLengthAllowed(textLine);

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