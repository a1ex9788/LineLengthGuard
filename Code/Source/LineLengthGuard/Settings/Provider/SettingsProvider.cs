using System;
using System.Collections.Generic;
using LineLengthGuard.Settings.Parser;

namespace LineLengthGuard.Settings.Provider
{
    internal sealed class SettingsProvider : ISettingsProvider
    {
        private readonly Dictionary<int, ISettings> settingsByFile = [];

        private readonly ISettingsParser settingsParser;

        public SettingsProvider(ISettingsParser settingsParser)
        {
            this.settingsParser = settingsParser;
        }

        public ISettings Get(string settingsJSON)
        {
            int hashCode = StringComparer.Ordinal.GetHashCode(settingsJSON);

            this.settingsByFile.TryGetValue(hashCode, out ISettings cachedSettings);

            if (cachedSettings is not null)
            {
                return cachedSettings;
            }

            ISettings? settings = this.settingsParser.Parse(settingsJSON);

            settings ??= new FileSettings();

            this.settingsByFile[hashCode] = settings;

            return settings;
        }
    }
}