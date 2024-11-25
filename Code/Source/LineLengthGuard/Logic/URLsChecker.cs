using System;
using System.Text.RegularExpressions;

namespace LineLengthGuard.Logic
{
    internal static class URLsChecker
    {
        private const string UrlPattern = "http[s]?://.+";

        private static readonly Regex Regex = new Regex(UrlPattern, RegexOptions.None, TimeSpan.FromSeconds(1));

        public static bool ContainsURL(string line)
        {
            return Regex.IsMatch(line);
        }
    }
}