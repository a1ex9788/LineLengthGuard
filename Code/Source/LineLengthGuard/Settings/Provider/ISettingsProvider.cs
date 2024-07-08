namespace LineLengthGuard.Settings.Provider
{
    internal interface ISettingsProvider
    {
        ISettings Get(string settingsJSON);
    }
}