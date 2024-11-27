using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Provider;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace LineLengthGuard.Tests.UnitTests.Settings.Provider
{
    internal static class SettingsProviderTestUtilities
    {
        private static readonly FieldInfo SettingsByFileField = typeof(SettingsProvider)
            .GetField("settingsByFile", BindingFlags.NonPublic | BindingFlags.Instance)!;

        public static string GetSettingsJSON(ISettings settings)
        {
            return JsonSerializer.Serialize(settings);
        }

        public static Dictionary<int, ISettings> GetSettingsByFileField(SettingsProvider settingsProvider)
        {
            return (Dictionary<int, ISettings>)SettingsByFileField.GetValue(settingsProvider)!;
        }

        public static void SetSettingsByFileField(
            SettingsProvider settingsProvider,
            Dictionary<int, ISettings> value)
        {
            SettingsByFileField.SetValue(settingsProvider, value);
        }
    }
}