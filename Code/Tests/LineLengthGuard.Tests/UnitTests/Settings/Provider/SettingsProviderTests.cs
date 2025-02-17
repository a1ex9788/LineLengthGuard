using FluentAssertions;
using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Parser;
using LineLengthGuard.Settings.Provider;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Concurrent;
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

            ConcurrentDictionary<string, ISettings> expectedSettingsByFilePath =
                new ConcurrentDictionary<string, ISettings>(StringComparer.Ordinal);

            expectedSettingsByFilePath.TryAdd(this.settingsFilePath, expectedSettings);

            SettingsProviderTestUtilities
                .GetSettingsByFilePathField(settingsProvider)
                .Should()
                .BeEquivalentTo(expectedSettingsByFilePath);
        }

        [TestMethod]
        public void Get_SettingsNotCachedAndInvalidJSON_ThrowsException()
        {
            // Arrange.
            string settingsFileContent = "Invalid JSON.";

            AdditionalText settingsFile = SettingsProviderTestUtilities
                .GetSettingsFile(this.settingsFilePath, settingsFileContent);

            SettingsProvider settingsProvider = new SettingsProvider(new SettingsParser());

            // Act.
            Action action = () => settingsProvider.Get(settingsFile, CancellationToken.None);

            // Assert.
            action
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage($"Content of settings file '{this.settingsFilePath}' has an invalid format.");
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

            ConcurrentDictionary<string, ISettings> settingsByFilePath =
                new ConcurrentDictionary<string, ISettings>(StringComparer.Ordinal);

            settingsByFilePath.TryAdd(this.settingsFilePath, expectedSettings);
            settingsByFilePath.TryAdd(this.anotherSettingsFilePath, Substitute.For<ISettings>());

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