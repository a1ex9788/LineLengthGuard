using FluentAssertions;
using LineLengthGuard.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LineLengthGuard.Tests.UnitTests
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
        public void IsAllowedMethodNameWithUnderscores_AllowAndContainsUnderscores_ReturnsTrue(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            MethodNamesChecker methodNamesChecker = new MethodNamesChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                    MaximumLineLength = 10,
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
        public void IsAllowedMethodNameWithUnderscores_AllowAndDoesNotContainUnderscores_ReturnsFalse(string line)
        {
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            MethodNamesChecker methodNamesChecker = new MethodNamesChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = true,
                    MaximumLineLength = 10,
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
            ArgumentNullException.ThrowIfNull(line);

            // Arrange.
            MethodNamesChecker methodNamesChecker = new MethodNamesChecker(
                new FileSettings
                {
                    AllowLongMethodNamesWithUnderscores = false,
                    MaximumLineLength = 10,
                });

            // Act.
            bool isAllowed = methodNamesChecker.IsAllowedMethodNameWithUnderscores(line);

            // Assert.
            isAllowed.Should().BeFalse();
        }
    }
}