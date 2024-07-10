using FluentAssertions;
using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Settings.Parser
{
    [TestClass]
    public class SettingsParserTests
    {
        [TestMethod]
        public void Parse_ValidSchema_ReturnsExpectedSettings()
        {
            // Arrange.
            string settingsJSON = """
{
  "ExcludedLineStarts": [
    "Value1",
    "Value2"
  ],
  "MaximumLineLength": 5
}
""";

            SettingsParser settingsParser = new SettingsParser();

            // Act.
            ISettings? settings = settingsParser.Parse(settingsJSON);

            // Assert.
            settings.Should().NotBeNull();

            settings!.ExcludedLineStarts.Should().BeEquivalentTo("Value1", "Value2");
            settings!.MaximumLineLength.Should().Be(5);
        }

        [TestMethod]
        public void Parse_InvalidSchema_ReturnsNull()
        {
            // Arrange.
            string settingsJSON = "Not valid schema.";

            SettingsParser settingsParser = new SettingsParser();

            // Act.
            ISettings? settings = settingsParser.Parse(settingsJSON);

            // Assert.
            settings.Should().BeNull();
        }
    }
}