using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CSVerifier = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerVerifier<
    LineLengthGuard.LLG001, Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace LineLengthGuard.Tests.IntegrationTests
{
    [TestClass]
    public class LLG001IntegrationTests
    {
        [TestMethod]
        public async Task LLG001_LinesShorterThanMaximumLength_ReportsNoDiagnostics()
        {
            string code = """
namespace TestNamespace
{
    class TestClass
    {
        // Shorter than maximum length.
        private string a = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjj";
        private string b = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrr";

        // Equal to maximum length.
        private string c = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrr";
    }
}
""";

            await CSVerifier.VerifyAnalyzerAsync(code);
        }

        [TestMethod]
        public async Task LLG001_LinesLongerThanMaximumLength_ReportsExpectedDiagnostics()
        {
            string code = """
namespace TestNamespace
{
    class TestClass
    {
        // Longer than maximum length.
        private string a = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrrs";
        private string b = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhhiiiiijjjjjkkkkklllllmmmmmnnnnnooooopppppqqqqqrrrrrsssss";
    }
}
""";
            DiagnosticResult[] expectedDiagnosticResults = [
                CSVerifier
                    .Diagnostic()
                    .WithSpan(startLine: 6, startColumn: 1, endLine: 6, endColumn: 122)
                    .WithArguments("120", "121"),
                CSVerifier
                    .Diagnostic()
                    .WithSpan(startLine: 7, startColumn: 1, endLine: 7, endColumn: 126)
                    .WithArguments("120", "125"),
            ];

            await CSVerifier.VerifyAnalyzerAsync(code, expectedDiagnosticResults);
        }
    }
}