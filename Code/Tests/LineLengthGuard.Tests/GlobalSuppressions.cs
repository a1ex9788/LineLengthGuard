using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Layout",
    "MEN002:Line is too long",
    Justification = "It is a test case.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests" +
        ".LLG001_LinesLongerThanMaximumLength_ReportsExpectedDiagnostics~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Layout",
    "MEN002:Line is too long",
    Justification = "It is a test case.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests" +
        ".LLG001_LinesShorterThanMaximumLength_ReportsNoDiagnostics~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Maintainability",
    "CA1515:Consider making public types internal",
    Justification = "Tests framework only detects tests in public classes.",
    Scope = "namespaceAndDescendants",
    Target = "~N:LineLengthGuard.Tests")]
[assembly: SuppressMessage(
    "Major Code Smell",
    "S4144:Methods should not have identical implementations",
    Justification = "It has different test cases",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.UnitTests.Logic.LinesLengthCheckerTests" +
        ".HasAllowedLineLength_DefaultExcludedLinePattern_ReturnsTrue(System.String)")]
[assembly: SuppressMessage(
    "Major Code Smell",
    "S4144:Methods should not have identical implementations",
    Justification = "It has different test cases",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.UnitTests.Logic.LinesLengthCheckerTests" +
        ".HasAllowedLineLength_NotDefaultExcludedLinePattern_ReturnsFalse(System.String)")]
[assembly: SuppressMessage(
    "Minor Code Smell",
    "S100:Methods and properties should be named in PascalCase",
    Justification = "JSON is an acronym.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.UnitTests.Settings.Provider.SettingsProviderTestUtilities" +
        ".GetSettingsJSON(LineLengthGuard.Settings.ISettings)~System.String")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell001:Spell Check",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell001:Spell Check",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests." +
        "LLG001_LinesLongerThanMaximumLength_ReportsExpectedDiagnostics~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell001:Spell Check",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests." +
        "LLG001_LinesShorterThanMaximumLength_ReportsNoDiagnostics~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell002:Ignore Word",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "type",
    Target = "~T:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell002:Ignore Word",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests." +
        "LLG001_LinesLongerThanMaximumLength_ReportsExpectedDiagnostics~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Naming",
    "VSSpell002:Ignore Word",
    Justification = "It is wanted the name that way to match diagnostic identifier.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.IntegrationTests.LLG001IntegrationTests." +
        "LLG001_LinesShorterThanMaximumLength_ReportsNoDiagnostics~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Style",
    "JSON002:Probable JSON string detected",
    Justification = "It is used for testing a method that receives a JSON as argument.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.UnitTests.Settings.Parser.SettingsParserTests" +
        ".Parse_ValidSchemaWithCollectionWithOneValue_ReturnsExpectedSettings")]
[assembly: SuppressMessage(
    "Style",
    "JSON002:Probable JSON string detected",
    Justification = "It is used for testing a method that receives a JSON as argument.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Tests.UnitTests.Settings.Parser.SettingsParserTests" +
        ".Parse_ValidSchemaWithCollectionWithMultipleValues_ReturnsExpectedSettings")]