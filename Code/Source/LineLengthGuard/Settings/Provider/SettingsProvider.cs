using System;
using System.Collections.Concurrent;
using System.Threading;
using LineLengthGuard.Settings.Parser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace LineLengthGuard.Settings.Provider
{
    internal sealed class SettingsProvider : ISettingsProvider
    {
        private readonly ConcurrentDictionary<string, ISettings> settingsByFilePath = [];

        private readonly ISettingsParser settingsParser;

        public SettingsProvider(ISettingsParser settingsParser)
        {
            this.settingsParser = settingsParser;
        }

        public ISettings Get(AdditionalText settingsFile, CancellationToken cancellationToken)
        {
            this.settingsByFilePath.TryGetValue(settingsFile.Path, out ISettings cachedSettings);

            if (cachedSettings is not null)
            {
                return cachedSettings;
            }

            ISettings settings = this.GetSettings(settingsFile, cancellationToken);

            this.settingsByFilePath[settingsFile.Path] = settings;

            return settings;
        }

        private ISettings GetSettings(AdditionalText settingsFile, CancellationToken cancellationToken)
        {
            SourceText settingsFileContent = settingsFile.GetText(cancellationToken)
                ?? throw new InvalidOperationException(
                    $"Content of settings file '{settingsFile.Path}' could not be read.");

            return this.settingsParser.Parse(settingsFileContent.ToString())
                ?? throw new InvalidOperationException(
                    $"Content of settings file '{settingsFile.Path}' has an invalid format.");
        }
    }
}