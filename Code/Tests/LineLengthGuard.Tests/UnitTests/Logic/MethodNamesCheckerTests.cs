using FluentAssertions;
using LineLengthGuard.Logic;
using LineLengthGuard.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Logic
{
    [TestClass]
    public class MethodNamesCheckerTests
    {
        [DataTestMethod]
        [DataRow("Aaa_Bbb_Ccc(")]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("    Aaa_Bbb_Ccc(")]
        [DataRow("    Aaa_Bbb_Ccc()")]
        [DataRow("        Aaa_Bbb_Ccc(")]
        [DataRow("        Aaa_Bbb_Ccc()")]
        [DataRow("Aaaaa_Bbbbb_Ccccc(")]
        [DataRow("Aaaaa_Bbbbb_Ccccc()")]
        [DataRow("    Aaaaa_Bbbbb_Ccccc(")]
        [DataRow("    Aaaaa_Bbbbb_Ccccc()")]
        [DataRow("        Aaaaa_Bbbbb_Ccccc(")]
        [DataRow("        Aaaaa_Bbbbb_Ccccc()")]
        public void IsAllowedMethodNameWithUnderscores_AllowAndMethodNameWithUnderscores_ReturnsTrue(string line)
        {
            // Arrange.
            MethodNamesChecker methodNamesChecker = new MethodNamesChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                });

            // Act.
            bool isAllowed = methodNamesChecker.IsAllowedMethodNameWithUnderscores(line);

            // Assert.
            isAllowed.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("AaaBbbCcc(")]
        [DataRow("AaaBbbCcc()")]
        [DataRow("AaaaaBbbbbCcccc(")]
        [DataRow("AaaaaBbbbbCcccc()")]
        public void IsAllowedMethodNameWithUnderscores_AllowAndNotMethodNameWithUnderscores_ReturnsFalse(string line)
        {
            // Arrange.
            MethodNamesChecker methodNamesChecker = new MethodNamesChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                });

            // Act.
            bool isAllowed = methodNamesChecker.IsAllowedMethodNameWithUnderscores(line);

            // Assert.
            isAllowed.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow("AaaBbbCcc(")]
        [DataRow("AaaBbbCcc()")]
        [DataRow("Aaa_Bbb_Ccc(")]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("AaaaaBbbbbCcccc(")]
        [DataRow("AaaaaBbbbbCcccc()")]
        [DataRow("Aaaaa_Bbbbb_Ccccc(")]
        [DataRow("Aaaaa_Bbbbb_Ccccc()")]
        public void IsAllowedMethodNameWithUnderscores_DoNotAllow_ReturnsFalse(string line)
        {
            // Arrange.
            MethodNamesChecker methodNamesChecker = new MethodNamesChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = false,
                });

            // Act.
            bool isAllowed = methodNamesChecker.IsAllowedMethodNameWithUnderscores(line);

            // Assert.
            isAllowed.Should().BeFalse();
        }
    }
}