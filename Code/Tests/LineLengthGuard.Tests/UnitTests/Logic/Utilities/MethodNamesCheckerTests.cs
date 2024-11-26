using FluentAssertions;
using LineLengthGuard.Logic.Utilities;
using LineLengthGuard.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Logic.Utilities
{
    [TestClass]
    public class MethodNamesCheckerTests
    {
        [DataTestMethod]
        [DataRow("Aaa_Bbb(")]
        [DataRow("Aaa_Bbb()")]
        [DataRow("Aaa_Bbb_Ccc(")]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("private void Aaa_Bbb(")]
        [DataRow("private void Aaa_Bbb()")]
        [DataRow("private void Aaa_Bbb_Ccc(")]
        [DataRow("private void Aaa_Bbb_Ccc()")]
        [DataRow("    Aaa_Bbb(")]
        [DataRow("    Aaa_Bbb()")]
        [DataRow("    Aaa_Bbb_Ccc(")]
        [DataRow("    Aaa_Bbb_Ccc()")]
        [DataRow("    private void Aaa_Bbb(")]
        [DataRow("    private void Aaa_Bbb()")]
        [DataRow("    private void Aaa_Bbb_Ccc(")]
        [DataRow("    private void Aaa_Bbb_Ccc()")]
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
        [DataRow("AaaBbb")]
        [DataRow("AaaBbb(")]
        [DataRow("AaaBbb()")]
        [DataRow("private void AaaBbb")]
        [DataRow("private voidAaaBbb(")]
        [DataRow("private voidAaaBbb()")]
        [DataRow("Aaa_Bbb(c")]
        [DataRow("Aaa_Bbb(ccc")]
        [DataRow("Aaa_Bbb()c")]
        [DataRow("Aaa_Bbb()ccc")]
        [DataRow("    AaaBbb")]
        [DataRow("    AaaBbb(")]
        [DataRow("    AaaBbb()")]
        [DataRow("    private void AaaBbb")]
        [DataRow("    private voidAaaBbb(")]
        [DataRow("    private voidAaaBbb()")]
        [DataRow("    Aaa_Bbb(c")]
        [DataRow("    Aaa_Bbb(ccc")]
        [DataRow("    Aaa_Bbb()c")]
        [DataRow("    Aaa_Bbb()ccc")]
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
        [DataRow("Aaa_Bbb(")]
        [DataRow("Aaa_Bbb()")]
        [DataRow("Aaa_Bbb_Ccc(")]
        [DataRow("Aaa_Bbb_Ccc()")]
        [DataRow("private void Aaa_Bbb(")]
        [DataRow("private void Aaa_Bbb()")]
        [DataRow("private void Aaa_Bbb_Ccc(")]
        [DataRow("private void Aaa_Bbb_Ccc()")]
        [DataRow("    Aaa_Bbb(")]
        [DataRow("    Aaa_Bbb()")]
        [DataRow("    Aaa_Bbb_Ccc(")]
        [DataRow("    Aaa_Bbb_Ccc()")]
        [DataRow("    private void Aaa_Bbb(")]
        [DataRow("    private void Aaa_Bbb()")]
        [DataRow("    private void Aaa_Bbb_Ccc(")]
        [DataRow("    private void Aaa_Bbb_Ccc()")]
        [DataRow("AaaBbb")]
        [DataRow("AaaBbb(")]
        [DataRow("AaaBbb()")]
        [DataRow("private void AaaBbb")]
        [DataRow("private voidAaaBbb(")]
        [DataRow("private voidAaaBbb()")]
        [DataRow("Aaa_Bbb(c")]
        [DataRow("Aaa_Bbb(ccc")]
        [DataRow("Aaa_Bbb()c")]
        [DataRow("Aaa_Bbb()ccc")]
        [DataRow("    AaaBbb")]
        [DataRow("    AaaBbb(")]
        [DataRow("    AaaBbb()")]
        [DataRow("    private void AaaBbb")]
        [DataRow("    private voidAaaBbb(")]
        [DataRow("    private voidAaaBbb()")]
        [DataRow("    Aaa_Bbb(c")]
        [DataRow("    Aaa_Bbb(ccc")]
        [DataRow("    Aaa_Bbb()c")]
        [DataRow("    Aaa_Bbb()ccc")]
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