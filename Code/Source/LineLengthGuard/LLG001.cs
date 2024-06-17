using LineLengthGuard.Properties;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;

namespace LineLengthGuard
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LLG001 : DiagnosticAnalyzer
    {
        public const string DiagnosticId = nameof(LLG001);
        private const string Category = "Naming";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be
        // localize-able, you can use regular strings for Title and MessageFormat. See
        // https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization.
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

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.EnableConcurrentExecution();

            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols. See
            // https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Actions%20Semantics.md for more
            // information.
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext symbolAnalysisContext)
        {
            // TODO: Replace the following code with your own analysis, generating Diagnostic objects for any issues you
            // find.
            ISymbol namedTypeSymbol = (INamedTypeSymbol)symbolAnalysisContext.Symbol;

            if (Array.Exists(namedTypeSymbol.Name.ToCharArray(), char.IsLower))
            {
                // For all such symbols, produce a diagnostic.
                Diagnostic diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);

                symbolAnalysisContext.ReportDiagnostic(diagnostic);
            }
        }
    }
}