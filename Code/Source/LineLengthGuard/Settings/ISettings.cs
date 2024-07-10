using System.Collections.Generic;

namespace LineLengthGuard.Settings
{
    public interface ISettings
    {
        IReadOnlyCollection<string> ExcludedLineStarts { get; }

        int MaximumLineLength { get; }
    }
}