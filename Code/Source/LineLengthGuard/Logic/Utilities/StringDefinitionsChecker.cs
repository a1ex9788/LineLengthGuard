using LineLengthGuard.Settings;

namespace LineLengthGuard.Logic.Utilities
{
    internal sealed class StringDefinitionsChecker : RegexMatchesChecker
    {
        private readonly ISettings settings;

        public StringDefinitionsChecker(ISettings settings)
            : base("^(\\s)*\".+\";$")
        {
            this.settings = settings;
        }

        public bool IsAllowedLongStringDefinition(string line)
        {
            return this.settings.AllowLongStringDefinitions && this.IsMatch(line);
        }
    }
}