using Microsoft.CodeAnalysis.Text;

namespace LineLengthGuard
{
    internal static class AllowedLineLengthChecker
    {
        public const int MaximumLineLength = 120;

        public static (bool IsAllowed, int LineLength) IsLineLengthAllowed(TextLine textLine)
        {
            int lineLength = textLine.ToString().Length;

            return (lineLength <= MaximumLineLength, lineLength);
        }
    }
}