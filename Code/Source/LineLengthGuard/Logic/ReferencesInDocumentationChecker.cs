using LineLengthGuard.Logic.Utilities;

namespace LineLengthGuard.Logic
{
    internal sealed class ReferencesInDocumentationChecker : RegexMatchesChecker
    {
        public ReferencesInDocumentationChecker()
            : base("<see cref=\".+\"/>")
        {
        }

        public bool ContainsReferenceInDocumentation(string line)
        {
            return this.IsMatch(line);
        }
    }
}