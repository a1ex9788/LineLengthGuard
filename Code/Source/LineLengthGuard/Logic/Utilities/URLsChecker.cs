namespace LineLengthGuard.Logic.Utilities
{
    internal sealed class URLsChecker : RegexMatchesChecker
    {
        public URLsChecker()
            : base("http[s]?://[^\\s]+")
        {
        }

        public bool ContainsURL(string line)
        {
            return this.IsMatch(line);
        }
    }
}