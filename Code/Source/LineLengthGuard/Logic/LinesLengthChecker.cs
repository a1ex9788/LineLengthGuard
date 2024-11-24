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

        public LinesLengthChecker(ISettings settings, MethodNamesChecker methodNamesChecker)
        {
            this.settings = settings;
            this.methodNamesChecker = methodNamesChecker;
        }

        public (bool IsAllowed, int LineLength) HasAllowedLineLength(TextLine textLine)
        {
            string line = textLine.ToString();

            bool isAllowed = this.IsLineLengthShorterThanMaximum(line)
                || this.IsLineStartExcluded(line)
                || this.methodNamesChecker.IsAllowedMethodNameWithUnderscores(line);

            return (isAllowed, line.Length);
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