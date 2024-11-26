using System;
using System.Text.RegularExpressions;

namespace LineLengthGuard.Logic.Utilities
{
    internal class RegexMatchesChecker
    {
        private readonly Regex regex;

        public RegexMatchesChecker(string pattern)
        {
            this.regex = new Regex(pattern, RegexOptions.None, TimeSpan.FromMilliseconds(500));
        }

        protected bool IsMatch(string @string)
        {
            return this.regex.IsMatch(@string);
        }
    }
}