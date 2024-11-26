using LineLengthGuard.Logic.Utilities;
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
        private readonly ReferencesInDocumentationChecker referencesInDocumentationChecker;
        private readonly StringDefinitionsChecker stringDefinitionsChecker;
        private readonly URLsChecker urlsChecker;

        public LinesLengthChecker(
            ISettings settings,
            MethodNamesChecker methodNamesChecker,
            ReferencesInDocumentationChecker referencesInDocumentationChecker,
            StringDefinitionsChecker stringDefinitionsChecker,
            URLsChecker urlsChecker)
        {
            this.settings = settings;
            this.methodNamesChecker = methodNamesChecker;
            this.referencesInDocumentationChecker = referencesInDocumentationChecker;
            this.stringDefinitionsChecker = stringDefinitionsChecker;
            this.urlsChecker = urlsChecker;
        }

        public (bool IsAllowed, int LineLength) HasAllowedLineLength(TextLine textLine)
        {
            string line = textLine.ToString();

            bool isAllowed = this.IsLineLengthShorterThanMaximum(line)
                || this.ContainsDefaultExcludedLinePattern(line)
                || this.IsLineStartExcluded(line)
                || this.methodNamesChecker.IsAllowedMethodNameWithUnderscores(line)
                || this.stringDefinitionsChecker.IsAllowedLongStringDefinition(line);

            return (isAllowed, line.Length);
        }

        private bool IsLineLengthShorterThanMaximum(string line)
        {
            return line.Length <= this.settings.MaximumLineLength;
        }

        private bool ContainsDefaultExcludedLinePattern(string line)
        {
            return this.referencesInDocumentationChecker.ContainsReferenceInDocumentation(line)
                || this.urlsChecker.ContainsURL(line);
        }

        private bool IsLineStartExcluded(string line)
        {
            return this.settings.ExcludedLineStarts.Any(s => line.StartsWith(s, StringComparison.Ordinal));
        }
    }
}