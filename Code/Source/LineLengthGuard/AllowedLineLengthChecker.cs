using LineLengthGuard.Settings;
using Microsoft.CodeAnalysis.Text;

namespace LineLengthGuard
{
    internal sealed class AllowedLineLengthChecker
    {
        private readonly ISettings settings;

        public AllowedLineLengthChecker(ISettings settings)
        {
            this.settings = settings;
        }

        public (bool IsAllowed, int LineLength) IsLineLengthAllowed(TextLine textLine)
        {
            int lineLength = textLine.ToString().Length;

            return (lineLength <= this.settings.MaximumLineLength, lineLength);
        }
    }
}