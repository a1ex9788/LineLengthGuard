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

            bool excluded = this.settings.ExcludedLineStarts.Any(s => line.StartsWith(s, StringComparison.Ordinal));

            return (excluded || line.Length <= this.settings.MaximumLineLength, line.Length);
        }
    }
}