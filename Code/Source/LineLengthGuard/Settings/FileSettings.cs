using System.Collections.Generic;

namespace LineLengthGuard.Settings
{
    internal sealed record FileSettings : ISettings
    {
        private const int DefaultMaximumLineLength = 120;

        public IReadOnlyCollection<string> ExcludedLineStarts { get; set; } = [];

        public int MaximumLineLength { get; set; } = DefaultMaximumLineLength;
    }
}