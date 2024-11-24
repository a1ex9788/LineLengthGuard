using System.Collections.Generic;

namespace LineLengthGuard.Settings
{
    public interface ISettings
    {
        bool AllowLongMethodNamesWithUnderscores { get; }

        bool AllowLongStringDefinitions { get; }

        IReadOnlyCollection<string> ExcludedLineStarts { get; }

        int MaximumLineLength { get; }
    }
}