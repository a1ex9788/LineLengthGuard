using System.Diagnostics.CodeAnalysis;

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