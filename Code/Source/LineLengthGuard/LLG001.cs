using LineLengthGuard.Settings;
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
        private static readonly ISettings Settings = new FileSettings();

        private static readonly AllowedLineLengthChecker AllowedLineLengthChecker =
            new AllowedLineLengthChecker(Settings);

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
                (bool isAllowed, int lineLength) = AllowedLineLengthChecker.IsLineLengthAllowed(textLine);

                if (!isAllowed)
                {
                    ReportDiagnostic(context, textLine, lineLength);
                }
            }
        }

        private static void ReportDiagnostic(SyntaxTreeAnalysisContext context, TextLine textLine, int lineLength)
        {
            Location location = Location.Create(context.Tree, textLine.Span);

            Diagnostic diagnostic = Diagnostic
                .Create(LLG001Info.Rule, location, Settings.MaximumLineLength, lineLength);

            context.ReportDiagnostic(diagnostic);
        }
    }
}