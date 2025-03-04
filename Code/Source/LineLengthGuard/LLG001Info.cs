using LineLengthGuard.Properties;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace LineLengthGuard
{
    internal static class LLG001Info
    {
        private const string DiagnosticId = nameof(LLG001);

        private const string Category = "Style";

        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.AnalyzerTitle),
            Resources.ResourceManager,
            typeof(Resources));

        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(
            nameof(Resources.AnalyzerMessageFormat),
            Resources.ResourceManager,
            typeof(Resources));

        private static readonly LocalizableString Description = new LocalizableResourceString(
            nameof(Resources.AnalyzerDescription),
            Resources.ResourceManager,
            typeof(Resources));

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            Description);

        public static readonly ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics = [Rule];
    }
}