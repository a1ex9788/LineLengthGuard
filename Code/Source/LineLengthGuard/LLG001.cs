using LineLengthGuard.Logic;
using LineLengthGuard.Logic.Utilities;
using LineLengthGuard.Settings;
using LineLengthGuard.Settings.Parser;
using LineLengthGuard.Settings.Provider;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace LineLengthGuard
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LLG001 : DiagnosticAnalyzer
    {
        // Static field holds an instance to avoid recreating the object.
        private static readonly SettingsProvider SettingsProvider = new SettingsProvider(new SettingsParser());

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => LLG001Info.SupportedDiagnostics;

        public override void Initialize(AnalysisContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.EnableConcurrentExecution();

            // Registered action is executed once for every code document.
            context.RegisterSyntaxTreeAction(AnalyzeSyntaxTree);
        }

        private static void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext syntaxTreeAnalysisContext)
        {
            if (!syntaxTreeAnalysisContext.Tree.TryGetText(out SourceText? sourceText))
            {
                return;
            }

            ISettings settings = GetSettings(syntaxTreeAnalysisContext);

            LinesLengthChecker linesLengthChecker = new LinesLengthChecker(
                settings,
                new MethodNamesChecker(settings),
                new ReferencesInDocumentationChecker(),
                new StringDefinitionsChecker(settings),
                new URLsChecker());

            foreach (TextLine textLine in sourceText.Lines)
            {
                (bool isAllowed, int lineLength) = linesLengthChecker.HasAllowedLineLength(textLine);

                if (!isAllowed)
                {
                    ReportDiagnostic(syntaxTreeAnalysisContext, settings.MaximumLineLength, textLine, lineLength);
                }
            }
        }

        private static ISettings GetSettings(SyntaxTreeAnalysisContext syntaxTreeAnalysisContext)
        {
            AdditionalText[] settingsFiles = syntaxTreeAnalysisContext.Options.AdditionalFiles
                .Where(at => Path.GetFileName(at.Path) == Constants.SettingsFileName)
                .ToArray();

            if (settingsFiles.Length == 0)
            {
                return new FileSettings();
            }

            if (settingsFiles.Length > 1)
            {
                throw new InvalidOperationException();
            }

            SourceText sourceText = settingsFiles.Single().GetText(syntaxTreeAnalysisContext.CancellationToken)
                ?? throw new InvalidOperationException();

            return SettingsProvider.Get(sourceText.ToString());
        }

        private static void ReportDiagnostic(
            SyntaxTreeAnalysisContext syntaxTreeAnalysisContext,
            int maximumLineLength,
            TextLine textLine,
            int lineLength)
        {
            Location location = Location.Create(syntaxTreeAnalysisContext.Tree, textLine.Span);

            Diagnostic diagnostic = Diagnostic
                .Create(LLG001Info.Rule, location, maximumLineLength, lineLength);

            syntaxTreeAnalysisContext.ReportDiagnostic(diagnostic);
        }
    }
}