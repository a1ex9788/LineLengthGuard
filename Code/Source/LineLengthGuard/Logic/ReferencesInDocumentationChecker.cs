using System;
using System.Text.RegularExpressions;

namespace LineLengthGuard.Logic
{
    internal static class ReferencesInDocumentationChecker
    {
        private const string ReferenceInDocumentationPattern = "<see cref=\".+\"/>";

        private static readonly Regex Regex =
            new Regex(ReferenceInDocumentationPattern, RegexOptions.None, TimeSpan.FromSeconds(1));

        public static bool ContainsReferenceInDocumentation(string line)
        {
            return Regex.IsMatch(line);
        }
    }
}