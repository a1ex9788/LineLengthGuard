using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;

namespace LineLengthGuard
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LLG001 : DiagnosticAnalyzer
    {
        private const int MaximumLength = 120;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => LLG001Info.SupportedDiagnostics;

        public override void Initialize(AnalysisContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.EnableConcurrentExecution();

            context.RegisterSyntaxTreeAction(AnalyzeSyntaxTree);
        }

        private static void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            if (!context.Tree.TryGetText(out SourceText? sourceText))
            {
                return;
            }

            foreach (TextLine textLine in sourceText.Lines)
            {
                int length = textLine.ToString().Length;

                if (length > MaximumLength)
                {
                    Location location = Location.Create(context.Tree, textLine.Span);
                    Diagnostic diagnostic = Diagnostic.Create(LLG001Info.Rule, location, MaximumLength, length);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}