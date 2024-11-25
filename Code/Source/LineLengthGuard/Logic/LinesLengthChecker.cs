using LineLengthGuard.Settings;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;

namespace LineLengthGuard.Logic
{
    internal sealed class LinesLengthChecker
    {
        private readonly ISettings settings;
        private readonly MethodNamesChecker methodNamesChecker;
        private readonly StringDefinitionsChecker stringDefinitionsChecker;

        public LinesLengthChecker(
            ISettings settings,
            MethodNamesChecker methodNamesChecker,
            StringDefinitionsChecker stringDefinitionsChecker)
        {
            this.settings = settings;
            this.methodNamesChecker = methodNamesChecker;
            this.stringDefinitionsChecker = stringDefinitionsChecker;
        }

        public (bool IsAllowed, int LineLength) HasAllowedLineLength(TextLine textLine)
        {
            string line = textLine.ToString();

            bool isAllowed = this.IsLineLengthShorterThanMaximum(line)
                || ContainsDefaultExcludedLinePattern(line)
                || this.IsLineStartExcluded(line)
                || this.methodNamesChecker.IsAllowedMethodNameWithUnderscores(line)
                || this.stringDefinitionsChecker.IsAllowedLongStringDefinition(line);

            return (isAllowed, line.Length);
        }

        private static bool ContainsDefaultExcludedLinePattern(string line)
        {
            return ReferencesInDocumentationChecker.ContainsReferenceInDocumentation(line)
                || URLsChecker.ContainsURL(line);
        }

        private bool IsLineLengthShorterThanMaximum(string line)
        {
            return line.Length <= this.settings.MaximumLineLength;
        }

        private bool IsLineStartExcluded(string line)
        {
            return this.settings.ExcludedLineStarts.Any(s => line.StartsWith(s, StringComparison.Ordinal));
        }
    }
}