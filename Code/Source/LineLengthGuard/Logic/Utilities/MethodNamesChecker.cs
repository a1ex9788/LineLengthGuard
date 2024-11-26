using LineLengthGuard.Settings;

namespace LineLengthGuard.Logic.Utilities
{
    internal sealed class MethodNamesChecker : RegexMatchesChecker
    {
        private readonly ISettings settings;

        public MethodNamesChecker(ISettings settings)
            : base(@".*_.*\(\){0,1}$")
        {
            this.settings = settings;
        }

        public bool IsAllowedMethodNameWithUnderscores(string line)
        {
            return this.settings.AllowLongMethodNamesWithUnderscores && this.IsMatch(line);
        }
    }
}