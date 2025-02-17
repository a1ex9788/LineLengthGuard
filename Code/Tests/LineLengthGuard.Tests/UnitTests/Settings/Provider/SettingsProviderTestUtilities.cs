using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Provider;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using NSubstitute;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using System.Threading;

namespace LineLengthGuard.Tests.UnitTests.Settings.Provider
{
    internal static class SettingsProviderTestUtilities
    {
        private static readonly FieldInfo SettingsByFilePathField = typeof(SettingsProvider)
            .GetField("settingsByFilePath", BindingFlags.NonPublic | BindingFlags.Instance)!;

        public static AdditionalText GetSettingsFile(string settingsFilePath, ISettings settings)
        {
            return GetSettingsFile(settingsFilePath, JsonSerializer.Serialize(settings));
        }

        public static AdditionalText GetSettingsFile(string settingsFilePath, string settingsFileContent)
        {
            AdditionalText additionalText = Substitute.For<AdditionalText>();

            additionalText.Path.Returns(settingsFilePath);

            SourceText sourceText = SourceText.From(settingsFileContent);
            additionalText.GetText(Arg.Any<CancellationToken>()).Returns(sourceText);

            return additionalText;
        }

        public static ConcurrentDictionary<string, ISettings> GetSettingsByFilePathField(
            SettingsProvider settingsProvider)
        {
            return (ConcurrentDictionary<string, ISettings>)SettingsByFilePathField.GetValue(settingsProvider)!;
        }

        public static void SetSettingsByFilePathField(
            SettingsProvider settingsProvider,
            ConcurrentDictionary<string, ISettings> value)
        {
            SettingsByFilePathField.SetValue(settingsProvider, value);
        }
    }
}