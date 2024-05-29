using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests
{
    [TestClass]
    public class ProjectTests
    {
        [TestMethod]
        public void Project_Test_ReturnsExpectedResult()
        {
            // Arrange.
            int a = 5;
            int b = 0;

            // Act.
            int c = a - b;

            // Assert.
            c.Should().Be(5);
        }
    }
}