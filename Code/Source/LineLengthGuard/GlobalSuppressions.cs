using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Critical Code Smell",
    "S1067:Expressions should not be too complex",
    Justification = "It is okay.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Logic.LinesLengthChecker" +
        ".HasAllowedLineLength(Microsoft.CodeAnalysis.Text.TextLine)~System.ValueTuple{System.Boolean,System.Int32}")]
[assembly: SuppressMessage(
    "Design",
    "MA0051:Method is too long",
    Justification = "It is easier to understand in one long method.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Settings.Parser.SettingsParser" +
        ".GetFileSettingsFromParts(System.String[])~LineLengthGuard.Settings.FileSettings")]
[assembly: SuppressMessage(
    "MicrosoftCodeAnalysisCorrectness",
    "RS1035:Do not use APIs banned for analyzers",
    Justification = "TODO: False positive, https://github.com/dotnet/roslyn-analyzers/issues/6467.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Settings.Parser.SettingsParser.Parse(System.String)~LineLengthGuard.Settings" +
        ".ISettings")]
[assembly: SuppressMessage(
    "MicrosoftCodeAnalysisReleaseTracking",
    "RS2008:Enable analyzer release tracking",
    Justification = "It is not needed.",
    Scope = "member",
    Target = "~F:LineLengthGuard.LLG001Info.Rule")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S101:Types should be named in PascalCase",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.LLG001")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S101:Types should be named in PascalCase",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.LLG001Info")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S100:Methods and properties should be named in PascalCase",
    Justification = "False positive.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Logic.Utilities.URLsChecker.ContainsURL(System.String)~System.Boolean")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell001:Spell Check",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.LLG001")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell002:Ignore Word",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.LLG001")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.OrderingRules",
    "SA1202:Elements should be ordered by access",
    Justification = "It is needed to avoid S3263.",
    Scope = "member",
    Target = "~F:LineLengthGuard.LLG001Info.Rule")]
[assembly: SuppressMessage(
    "Style",
    "MA0007:Add a comma after the last value",
    Justification = "It is a false positive.",
    Scope = "member",
    Target = "~M:LineLengthGuard.LLG001.GetSettings(Microsoft.CodeAnalysis.Diagnostics.SyntaxTreeAnalysisContext)" +
        "~LineLengthGuard.Settings.ISettings")]