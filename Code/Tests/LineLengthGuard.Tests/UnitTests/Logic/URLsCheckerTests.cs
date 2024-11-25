using FluentAssertions;
using LineLengthGuard.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LineLengthGuard.Tests.UnitTests.Logic
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
            ArgumentNullException.ThrowIfNull(line);

            // Act.
            bool containsURL = URLsChecker.ContainsURL(line);

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
            ArgumentNullException.ThrowIfNull(line);

            // Act.
            bool containsURL = URLsChecker.ContainsURL(line);

            // Assert.
            containsURL.Should().BeFalse();
        }
    }
}