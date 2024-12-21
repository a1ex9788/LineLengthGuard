using FluentAssertions;
using LineLengthGuard.Logic.Utilities;
using LineLengthGuard.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Logic.Utilities
{
    [TestClass]
    public class StringDefinitionsCheckerTests
    {
        [DataTestMethod]
        [DataRow("\"aaa\",")]
        [DataRow("$\"aaa\",")]
        [DataRow("@\"aaa\",")]
        [DataRow("$@\"aaa\",")]
        [DataRow("@$\"aaa\",")]
        [DataRow("\"aaa\";")]
        [DataRow("$\"aaa\";")]
        [DataRow("@\"aaa\";")]
        [DataRow("$@\"aaa\";")]
        [DataRow("@$\"aaa\";")]
        [DataRow("    \"aaa\",")]
        [DataRow("    $\"aaa\",")]
        [DataRow("    @\"aaa\",")]
        [DataRow("    $@\"aaa\",")]
        [DataRow("    @$\"aaa\",")]
        [DataRow("    \"aaa\";")]
        [DataRow("    $\"aaa\";")]
        [DataRow("    @\"aaa\";")]
        [DataRow("    $@\"aaa\";")]
        [DataRow("    @$\"aaa\";")]
        public void IsAllowedLongStringDefinition_AllowAndLongStringDefinition_ReturnsTrue(string line)
        {
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
        [DataRow("aaa\",")]
        [DataRow("\"aaa,")]
        [DataRow("aaa\";")]
        [DataRow("\"aaa;")]
        [DataRow("\"aaa\"")]
        [DataRow("    aaa\",")]
        [DataRow("    \"aaa,")]
        [DataRow("    aaa\";")]
        [DataRow("    \"aaa;")]
        [DataRow("    \"aaa\"")]
        public void IsAllowedLongStringDefinition_AllowAndNotLongStringDefinition_ReturnsFalse(string line)
        {
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
        [DataRow("\"aaa\",")]
        [DataRow("$\"aaa\",")]
        [DataRow("@\"aaa\",")]
        [DataRow("$@\"aaa\",")]
        [DataRow("@$\"aaa\",")]
        [DataRow("\"aaa\";")]
        [DataRow("$\"aaa\";")]
        [DataRow("@\"aaa\";")]
        [DataRow("$@\"aaa\";")]
        [DataRow("@$\"aaa\";")]
        [DataRow("    \"aaa\",")]
        [DataRow("    $\"aaa\",")]
        [DataRow("    @\"aaa\",")]
        [DataRow("    $@\"aaa\",")]
        [DataRow("    @$\"aaa\",")]
        [DataRow("    \"aaa\";")]
        [DataRow("    $\"aaa\";")]
        [DataRow("    @\"aaa\";")]
        [DataRow("    $@\"aaa\";")]
        [DataRow("    @$\"aaa\";")]
        [DataRow("aaa\",")]
        [DataRow("\"aaa,")]
        [DataRow("aaa\";")]
        [DataRow("\"aaa;")]
        [DataRow("\"aaa\"")]
        [DataRow("    aaa\",")]
        [DataRow("    \"aaa,")]
        [DataRow("    aaa\";")]
        [DataRow("    \"aaa;")]
        [DataRow("    \"aaa\"")]
        public void IsAllowedLongStringDefinition_DoNotAllow_ReturnsFalse(string line)
        {
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