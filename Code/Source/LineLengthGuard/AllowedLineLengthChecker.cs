using LineLengthGuard.Settings;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;

namespace LineLengthGuard
{
    internal sealed class AllowedLineLengthChecker
    {
        private readonly ISettings settings;

        public AllowedLineLengthChecker(ISettings settings)
        {
            this.settings = settings;
        }

        public (bool IsAllowed, int LineLength) Check(TextLine textLine)
        {
            string line = textLine.ToString();

            bool isAllowed = this.IsLineLengthShorterThanMaximum(line) || this.IsLineStartExcluded(line);

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