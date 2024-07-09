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
using System.Threading;

namespace LineLengthGuard
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LLG001 : DiagnosticAnalyzer
    {
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

            context.RegisterSyntaxTreeAction(AnalyzeSyntaxTree);
        }

        private static void AnalyzeSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            if (!context.Tree.TryGetText(out SourceText? sourceText))
            {
                return;
            }

            ISettings settings = GetSettings(context);

            AllowedLineLengthChecker allowedLineLengthChecker = new AllowedLineLengthChecker(settings);

            foreach (TextLine textLine in sourceText.Lines)
            {
                (bool isAllowed, int lineLength) = allowedLineLengthChecker.Check(textLine);

                if (!isAllowed)
                {
                    ReportDiagnostic(context, settings.MaximumLineLength, textLine, lineLength);
                }
            }
        }

        private static ISettings GetSettings(SyntaxTreeAnalysisContext context)
        {
            AdditionalText[] settingsFiles = context.Options.AdditionalFiles
                .Where(at => Path.GetFileName(at.Path) == Constants.SettingsFileName).ToArray();

            if (settingsFiles.Length == 0)
            {
                return new FileSettings();
            }

            if (settingsFiles.Length > 1)
            {
                throw new InvalidOperationException();
            }

            using CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource(TimeSpan.FromSeconds(10));

            return SettingsProvider.Get(settingsFiles.Single().GetText(cancellationTokenSource.Token).ToString());
        }

        private static void ReportDiagnostic(
            SyntaxTreeAnalysisContext context, int maximumLineLength, TextLine textLine, int lineLength)
        {
            Location location = Location.Create(context.Tree, textLine.Span);

            Diagnostic diagnostic = Diagnostic
                .Create(LLG001Info.Rule, location, maximumLineLength, lineLength);

            context.ReportDiagnostic(diagnostic);
        }
    }
}