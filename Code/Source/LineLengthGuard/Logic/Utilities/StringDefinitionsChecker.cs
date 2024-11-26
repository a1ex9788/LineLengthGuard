using LineLengthGuard.Settings;

namespace LineLengthGuard.Logic.Utilities
{
    internal sealed class StringDefinitionsChecker
    {
        private readonly ISettings settings;

        private readonly string minimumString = "\"\";";

        public StringDefinitionsChecker(ISettings settings)
        {
            this.settings = settings;
        }

        public bool IsAllowedLongStringDefinition(string line)
        {
            return this.settings.AllowLongStringDefinitions && this.IsStringDefinition(line);
        }

        private bool IsStringDefinition(string line)
        {
            line = line.TrimStart(' ');

            if (line.Length < this.minimumString.Length)
            {
                return false;
            }

            return line[0] == '"' && line[line.Length - 1 - 1] == '"' && line[line.Length - 1] == ';';
        }
    }
}