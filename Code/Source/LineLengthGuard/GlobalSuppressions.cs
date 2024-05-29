using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Usage",
    "MA0032:Use an overload with a CancellationToken argument",
    Justification = "McMaster manages cancellation token.",
    Scope = "member",
    Target = "~M:LineLengthGuard.Program.Main(System.String[])~System.Threading.Tasks.Task{System.Int32}")]