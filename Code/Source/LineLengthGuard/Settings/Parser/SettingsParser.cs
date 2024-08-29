using System.Text.Json;

namespace LineLengthGuard.Settings.Parser
{
    internal sealed class SettingsParser : ISettingsParser
    {
        public ISettings? Parse(string settingsJSON)
        {
            try
            {
                return JsonSerializer.Deserialize<FileSettings>(settingsJSON);
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}