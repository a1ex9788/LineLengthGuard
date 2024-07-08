namespace LineLengthGuard.Settings
{
    internal sealed record FileSettings : ISettings
    {
        private const int DefaultMaximumLineLength = 120;

        public int MaximumLineLength { get; set; } = DefaultMaximumLineLength;
    }
}