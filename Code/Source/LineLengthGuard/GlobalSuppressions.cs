using System.Diagnostics.CodeAnalysis;

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