namespace LineLengthGuard.Settings.Parser
{
    internal interface ISettingsParser
    {
        ISettings? Parse(string settingsJSON);
    }
}