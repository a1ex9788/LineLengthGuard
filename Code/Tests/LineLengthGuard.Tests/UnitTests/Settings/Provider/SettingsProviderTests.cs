using FluentAssertions;
using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Parser;
using LineLengthGuard.Settings.Provider;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace LineLengthGuard.Tests.UnitTests.Settings.Provider
{
    [TestClass]
    public class SettingsProviderTests
    {
        private readonly string settingsFilePath = Path.Combine("A:", "Directory", "SettingsFile.txt");
        private readonly string anotherSettingsFilePath = Path.Combine("A:", "AnotherDirectory", "SettingsFile.txt");

        [TestMethod]
        public void Get_SettingsNotCachedAndValidJSON_ParsesSettingsAndReturnsIt()
        {
            // Arrange.
            ISettings expectedSettings = Substitute.For<ISettings>();
            expectedSettings.MaximumLineLength.Returns(5);

            AdditionalText settingsFile = SettingsProviderTestUtilities
                .GetSettingsFile(this.settingsFilePath, expectedSettings);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            // Act.
            ISettings? cachedSettings = settingsProvider.Get(settingsFile, CancellationToken.None);

            // Assert.
            cachedSettings.Should().BeEquivalentTo(expectedSettings);

            Dictionary<string, ISettings> expectedSettingsByFilePath =
                new Dictionary<string, ISettings>(StringComparer.Ordinal)
                {
                    { this.settingsFilePath, expectedSettings },
                };

            SettingsProviderTestUtilities
                .GetSettingsByFilePathField(settingsProvider)
                .Should()
                .BeEquivalentTo(expectedSettingsByFilePath);
        }

        [TestMethod]
        public void Get_SettingsNotCachedAndInvalidJSON_ReturnsDefaultSettings()
        {
            // Arrange.
            ISettings expectedSettings = Substitute.For<ISettings>();

            AdditionalText settingsFile = SettingsProviderTestUtilities
                .GetSettingsFile(this.settingsFilePath, expectedSettings);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            // Act.
            ISettings? settings = settingsProvider.Get(settingsFile, CancellationToken.None);

            // Assert.
            settings.Should().BeEquivalentTo(expectedSettings);

            Dictionary<string, ISettings> expectedSettingsByFilePath =
                new Dictionary<string, ISettings>(StringComparer.Ordinal)
                {
                    { this.settingsFilePath, settings },
                };

            SettingsProviderTestUtilities
                .GetSettingsByFilePathField(settingsProvider)
                .Should()
                .BeEquivalentTo(expectedSettingsByFilePath);
        }

        [TestMethod]
        public void Get_SettingsCachedAndValidJSON_ReturnsCachedSettings()
        {
            // Arrange.
            ISettings expectedSettings = Substitute.For<ISettings>();
            expectedSettings.MaximumLineLength.Returns(5);

            AdditionalText settingsFile = SettingsProviderTestUtilities
                .GetSettingsFile(this.settingsFilePath, expectedSettings);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            Dictionary<string, ISettings> settingsByFilePath =
                new Dictionary<string, ISettings>(StringComparer.Ordinal)
                {
                    { this.settingsFilePath, expectedSettings },
                    { this.anotherSettingsFilePath, Substitute.For<ISettings>() },
                };

            SettingsProviderTestUtilities.SetSettingsByFilePathField(settingsProvider, settingsByFilePath);

            // Act.
            ISettings? settings = settingsProvider.Get(settingsFile, CancellationToken.None);

            // Assert.
            settings.Should().Be(expectedSettings);

            SettingsProviderTestUtilities
                .GetSettingsByFilePathField(settingsProvider)
                .Should()
                .BeEquivalentTo(settingsByFilePath);
        }

        [TestMethod]
        public void Get_SettingsCachedAndInvalidJSON_ReturnsDefaultCachedSettings()
        {
            // Arrange.
            ISettings expectedSettings = Substitute.For<ISettings>();

            AdditionalText settingsFile = SettingsProviderTestUtilities
                .GetSettingsFile(this.settingsFilePath, expectedSettings);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            Dictionary<string, ISettings> settingsByFilePath =
                new Dictionary<string, ISettings>(StringComparer.Ordinal)
                {
                    { this.settingsFilePath, expectedSettings },
                    { this.anotherSettingsFilePath, Substitute.For<ISettings>() },
                };

            SettingsProviderTestUtilities.SetSettingsByFilePathField(settingsProvider, settingsByFilePath);

            // Act.
            ISettings? settings = settingsProvider.Get(settingsFile, CancellationToken.None);

            // Assert.
            settings.Should().Be(expectedSettings);

            SettingsProviderTestUtilities
                .GetSettingsByFilePathField(settingsProvider)
                .Should()
                .BeEquivalentTo(settingsByFilePath);
        }
    }
}