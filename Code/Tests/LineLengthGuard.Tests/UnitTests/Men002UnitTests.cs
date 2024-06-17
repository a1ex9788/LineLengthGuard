using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LineLengthGuard.Tests.UnitTests
{
    [TestClass]
    public class Men002UnitTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            string code = """
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class UPPER
    {
    }
}
""";

            await CSharpAnalyzerVerifier<LLG001, DefaultVerifier>.VerifyAnalyzerAsync(code);
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            string code = """
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class lower
    {
    }
}
""";
            DiagnosticResult[] expectedDiagnosticResults = [
                CSharpAnalyzerVerifier<LLG001, DefaultVerifier>.Diagnostic()
                    .WithSpan(startLine: 10, startColumn: 11, endLine: 10, endColumn: 16)
                    .WithArguments("lower"),
            ];

            await CSharpAnalyzerVerifier<LLG001, DefaultVerifier>.VerifyAnalyzerAsync(code, expectedDiagnosticResults);
        }
    }
}