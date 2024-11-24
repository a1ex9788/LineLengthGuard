using LineLengthGuard.Settings;
using System;
using System.Linq;

namespace LineLengthGuard.Logic
{
    internal sealed class MethodNamesChecker
    {
        private readonly ISettings settings;

        public MethodNamesChecker(ISettings settings)
        {
            this.settings = settings;
        }

        public bool IsAllowedMethodNameWithUnderscores(string line)
        {
            return this.settings.AllowLongMethodNamesWithUnderscores && IsMethodNameWithUnderscores(line);
        }

        private static bool IsMethodNameWithUnderscores(string line)
        {
            string? lastItem = line.Split(' ').LastOrDefault();

            if (lastItem is null)
            {
                return false;
            }

            return lastItem.Contains("_")
                && (lastItem.EndsWith("(", StringComparison.Ordinal)
                    || lastItem.EndsWith("()", StringComparison.Ordinal));
        }
    }
}