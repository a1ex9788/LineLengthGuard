using Microsoft.CodeAnalysis;
using System.Threading;

namespace LineLengthGuard.Settings.Provider
{
    internal interface ISettingsProvider
    {
        ISettings Get(AdditionalText settingsFile, CancellationToken cancellationToken);
    }
}