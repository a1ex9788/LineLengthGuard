using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Critical Code Smell",
    "S2339:Public constant members should not be used",
    Justification = "it is needed to be a constant.",
    Scope = "member",
    Target = "~F:LineLengthGuard.LLG001.DiagnosticId")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S101:Types should be named in PascalCase",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.LLG001")]
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