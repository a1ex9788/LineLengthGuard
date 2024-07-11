using System.Collections.Generic;

namespace LineLengthGuard.Settings
{
    public interface ISettings
    {
        bool AllowLongMethodNamesWithUnderscores { get; }

        IReadOnlyCollection<string> ExcludedLineStarts { get; }

        int MaximumLineLength { get; }
    }
}