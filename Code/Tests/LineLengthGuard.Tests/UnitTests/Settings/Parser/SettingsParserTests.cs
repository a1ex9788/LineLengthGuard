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
        public void Parse_ValidSchemaWithCollectionWithOneValue_ReturnsExpectedSettings()
        {
            // Arrange.
            string settingsJSON = """
{
  "AllowLongMethodNamesWithUnderscores": true,
  "ExcludedLineStarts": [
    "Value"
  ],
  "MaximumLineLength": 5
}
""";

            SettingsParser settingsParser = new SettingsParser();

            // Act.
            ISettings? settings = settingsParser.Parse(settingsJSON);

            // Assert.
            settings.Should().NotBeNull();

            settings!.AllowLongMethodNamesWithUnderscores.Should().BeTrue();
            settings!.ExcludedLineStarts.Should().BeEquivalentTo("Value");
            settings!.MaximumLineLength.Should().Be(5);
        }

        [TestMethod]
        public void Parse_ValidSchemaWithCollectionWithMultipleValues_ReturnsExpectedSettings()
        {
            // Arrange.
            string settingsJSON = """
{
  "AllowLongMethodNamesWithUnderscores": false,
  "ExcludedLineStarts": [
    "Value1",
    "Value2",
    "Value 3",
    "    Target = \"~"
  ],
  "MaximumLineLength": 55
}
""";

            SettingsParser settingsParser = new SettingsParser();

            // Act.
            ISettings? settings = settingsParser.Parse(settingsJSON);

            // Assert.
            settings.Should().NotBeNull();

            settings!.AllowLongMethodNamesWithUnderscores.Should().BeFalse();
            settings!.ExcludedLineStarts.Should().BeEquivalentTo("Value1", "Value2", "Value 3", "    Target = \"~");
            settings!.MaximumLineLength.Should().Be(55);
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