using FluentAssertions;
using LineLengthGuard.Logic.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Logic.Utilities
{
    [TestClass]
    public class URLsCheckerTests
    {
        [DataTestMethod]
        [DataRow("http://one.server")]
        [DataRow("http://a.different.server")]
        [DataRow("http://another-server")]
        [DataRow("https://one.server")]
        [DataRow("https://a.different.server")]
        [DataRow("https://another-server")]
        [DataRow("///    http://one.server")]
        public void ContainsURL_URL_ReturnsTrue(string line)
        {
            // Arrange.
            URLsChecker urlsChecker = new URLsChecker();

            // Act.
            bool containsURL = urlsChecker.ContainsURL(line);

            // Assert.
            containsURL.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("http://")]
        [DataRow("https://")]
        [DataRow("htt://server")]
        [DataRow("///    http://")]
        public void ContainsURL_NotURL_ReturnsFalse(string line)
        {
            // Arrange.
            URLsChecker urlsChecker = new URLsChecker();

            // Act.
            bool containsURL = urlsChecker.ContainsURL(line);

            // Assert.
            containsURL.Should().BeFalse();
        }
    }
}