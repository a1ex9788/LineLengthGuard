using FluentAssertions;
using LineLengthGuard.Logic;
using LineLengthGuard.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LineLengthGuard.Tests.UnitTests.Logic
{
    [TestClass]
    public class StringDefinitionsCheckerTests
    {
        [DataTestMethod]
        [DataRow("\"aaaaabbbbb\";")]
        [DataRow("\"aaaaabbbbbccccc\";")]
        [DataRow("\"aaaaabbbbbcccccddddd\";")]
        [DataRow("        \"aaaaabbbbb\";")]
        public void IsAllowedLongStringDefinition_AllowAndLongStringDefinition_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            StringDefinitionsChecker stringDefinitionsChecker = new StringDefinitionsChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = true,
                });

            // Act.
            bool isAllowed = stringDefinitionsChecker.IsAllowedLongStringDefinition(line);

            // Assert.
            isAllowed.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("Aaaaa\";")]
        [DataRow("\"Aaaaa;")]
        [DataRow("\"Aaaaa\"")]
        [DataRow("aaaaabbbbb\";")]
        [DataRow("\"aaaaabbbbb;")]
        [DataRow("\"aaaaabbbbb\"")]
        [DataRow("aaaaabbbbbccccc\";")]
        [DataRow("\"aaaaabbbbbccccc;")]
        [DataRow("\"aaaaabbbbbccccc\"")]
        [DataRow("        aaaaabbbbb\";")]
        [DataRow("        \"aaaaabbbbb;")]
        [DataRow("        \"aaaaabbbbb\"")]
        public void IsAllowedLongStringDefinition_AllowAndNotLongStringDefinition_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            StringDefinitionsChecker stringDefinitionsChecker = new StringDefinitionsChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = true,
                });

            // Act.
            bool isAllowed = stringDefinitionsChecker.IsAllowedLongStringDefinition(line);

            // Assert.
            isAllowed.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow("\"Aaaaa\";")]
        [DataRow("\"aaaaabbbbb\";")]
        [DataRow("\"aaaaabbbbbccccc\";")]
        [DataRow("        \"aaaaabbbbb\";")]
        [DataRow("Aaaaa\";")]
        [DataRow("\"Aaaaa;")]
        [DataRow("\"Aaaaa\"")]
        [DataRow("aaaaabbbbb\";")]
        [DataRow("\"aaaaabbbbb;")]
        [DataRow("\"aaaaabbbbb\"")]
        [DataRow("aaaaabbbbbccccc\";")]
        [DataRow("\"aaaaabbbbbccccc;")]
        [DataRow("\"aaaaabbbbbccccc\"")]
        public void IsAllowedLongStringDefinition_DoNotAllow_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            StringDefinitionsChecker stringDefinitionsChecker = new StringDefinitionsChecker(
                new FileSettings
                {
                    AllowLongStringDefinitions = false,
                });

            // Act.
            bool isAllowed = stringDefinitionsChecker.IsAllowedLongStringDefinition(line);

            // Assert.
            isAllowed.Should().BeFalse();
        }
    }
}