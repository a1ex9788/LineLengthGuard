namespace LineLengthGuard.Logic.Utilities
{
    internal sealed class ReferencesInDocumentationChecker : RegexMatchesChecker
    {
        public ReferencesInDocumentationChecker()
            : base("<see cref=\"[^\\s]+\"/>")
        {
        }

        public bool ContainsReferenceInDocumentation(string line)
        {
            return this.IsMatch(line);
        }
    }
}