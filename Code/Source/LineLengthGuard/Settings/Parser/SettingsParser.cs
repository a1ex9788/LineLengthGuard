using Newtonsoft.Json;

namespace LineLengthGuard.Settings.Parser
{
    internal sealed class SettingsParser : ISettingsParser
    {
        public ISettings? Parse(string settingsJSON)
        {
            try
            {
                // Newtonsoft.Json is used instead of System.Text.Json to avoid execution errors saying its assembly is
                // not found. It is thought that they are produced because of the use of different frameworks in the
                // analyser and the projects that use them.
                return JsonConvert.DeserializeObject<FileSettings>(settingsJSON);
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}