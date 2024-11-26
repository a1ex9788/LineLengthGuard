using LineLengthGuard.Logic.Utilities;

namespace LineLengthGuard.Logic
{
    internal sealed class URLsChecker : RegexMatchesChecker
    {
        public URLsChecker()
            : base("http[s]?://.+")
        {
        }

        public bool ContainsURL(string line)
        {
            return this.IsMatch(line);
        }
    }
}