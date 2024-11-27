using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Provider;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using NSubstitute;
using System.Collections.Generic;
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
            AdditionalText additionalText = Substitute.For<AdditionalText>();

            additionalText.Path.Returns(settingsFilePath);

            SourceText sourceText = SourceText.From(JsonSerializer.Serialize(settings));
            additionalText.GetText(Arg.Any<CancellationToken>()).Returns(sourceText);

            return additionalText;
        }

        public static Dictionary<string, ISettings> GetSettingsByFilePathField(SettingsProvider settingsProvider)
        {
            return (Dictionary<string, ISettings>)SettingsByFilePathField.GetValue(settingsProvider)!;
        }

        public static void SetSettingsByFilePathField(
            SettingsProvider settingsProvider,
            Dictionary<string, ISettings> value)
        {
            SettingsByFilePathField.SetValue(settingsProvider, value);
        }
    }
}