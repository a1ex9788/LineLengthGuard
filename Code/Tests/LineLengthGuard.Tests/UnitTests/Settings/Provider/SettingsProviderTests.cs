using FluentAssertions;
using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Parser;
using LineLengthGuard.Settings.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LineLengthGuard.Tests.UnitTests.Settings.Provider
{
    [TestClass]
    public class SettingsProviderTests
    {
        [TestMethod]
        public void Get_SettingsNotCachedAndValidJSON_ParsesSettingsAndReturnsIt()
        {
            // Arrange.
            FileSettings expectedSettings = new FileSettings { MaximumLineLength = 5 };

            string settingsJSON = GetSettingsJSON(expectedSettings);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            // Act.
            ISettings? cachedSettings = settingsProvider.Get(settingsJSON);

            // Assert.
            cachedSettings.Should().BeEquivalentTo(expectedSettings);

            Dictionary<int, ISettings> expectedSettingsByFile = new Dictionary<int, ISettings>
            {
                { StringComparer.Ordinal.GetHashCode(settingsJSON), expectedSettings },
            };

            SettingsProviderTestUtilities.GetSettingsByFileField(settingsProvider)
                .Should().BeEquivalentTo(expectedSettingsByFile);
        }

        [TestMethod]
        public void Get_SettingsNotCachedAndInvalidJSON_ReturnsDefaultSettings()
        {
            // Arrange.
            FileSettings expectedSettings = new FileSettings();

            string settingsJSON = "Invalid JSON";

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            // Act.
            ISettings? cachedSettings = settingsProvider.Get(settingsJSON);

            // Assert.
            cachedSettings.Should().Be(expectedSettings);

            Dictionary<int, ISettings> expectedSettingsByFile = new Dictionary<int, ISettings>
            {
                { StringComparer.Ordinal.GetHashCode(settingsJSON), expectedSettings },
            };

            SettingsProviderTestUtilities.GetSettingsByFileField(settingsProvider)
                .Should().BeEquivalentTo(expectedSettingsByFile);
        }

        [TestMethod]
        public void Get_SettingsCachedAndValidJSON_ReturnsCachedSettings()
        {
            // Arrange.
            FileSettings expectedSettings = new FileSettings();

            string settingsJSON = GetSettingsJSON(expectedSettings);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            Dictionary<int, ISettings> settingsByFile = new Dictionary<int, ISettings>
            {
                { StringComparer.Ordinal.GetHashCode(settingsJSON), expectedSettings },
                { 123_456_789, new FileSettings { MaximumLineLength = 5 } },
            };
            SettingsProviderTestUtilities.SetSettingsByFileField(settingsProvider, settingsByFile);

            // Act.
            ISettings? cachedSettings = settingsProvider.Get(settingsJSON);

            // Assert.
            cachedSettings.Should().Be(expectedSettings);

            SettingsProviderTestUtilities.GetSettingsByFileField(settingsProvider)
                .Should().BeEquivalentTo(settingsByFile);
        }

        [TestMethod]
        public void Get_SettingsCachedAndInvalidJSON_ReturnsDefaultCachedSettings()
        {
            // Arrange.
            FileSettings expectedSettings = new FileSettings();

            string settingsJSON = "Invalid JSON";

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            Dictionary<int, ISettings> settingsByFile = new Dictionary<int, ISettings>
            {
                { StringComparer.Ordinal.GetHashCode(settingsJSON), expectedSettings },
                { 123_456_789, new FileSettings { MaximumLineLength = 5 } },
            };
            SettingsProviderTestUtilities.SetSettingsByFileField(settingsProvider, settingsByFile);

            // Act.
            ISettings? cachedSettings = settingsProvider.Get(settingsJSON);

            // Assert.
            cachedSettings.Should().Be(expectedSettings);

            SettingsProviderTestUtilities.GetSettingsByFileField(settingsProvider)
                .Should().BeEquivalentTo(settingsByFile);
        }

        private static string GetSettingsJSON(ISettings settings)
        {
            return JsonSerializer.Serialize(settings);
        }
    }
}