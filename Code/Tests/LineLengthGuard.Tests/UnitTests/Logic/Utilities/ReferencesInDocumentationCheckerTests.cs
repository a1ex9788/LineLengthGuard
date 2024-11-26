using FluentAssertions;
using LineLengthGuard.Logic.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Logic.Utilities
{
    [TestClass]
    public class ReferencesInDocumentationCheckerTests
    {
        [DataTestMethod]
        [DataRow("<see cref=\"ClassReference\"/>")]
        [DataRow("<see cref=\"Namespace.ClassReference\"/>")]
        [DataRow("<see cref=\"A.Namespace.ClassReference\"/>")]
        [DataRow("<see cref=\"MethodReference()\"/>")]
        [DataRow("<see cref=\"Class.MethodReference()\"/>")]
        [DataRow("<see cref=\"Namespace.Class.MethodReference()\"/>")]
        [DataRow("///    <see cref=\"ClassReference\"/>")]
        [DataRow("<see cref=\"ClassReference\"/> aaaaa")]
        [DataRow("///    <see cref=\"ClassReference\"/> aaaaa")]
        public void ContainsReferenceInDocumentation_ReferenceInDocumentation_ReturnsTrue(string line)
        {
            // Arrange.
            ReferencesInDocumentationChecker referencesInDocumentationChecker = new ReferencesInDocumentationChecker();

            // Act.
            bool containsReferenceInDocumentation = referencesInDocumentationChecker
                .ContainsReferenceInDocumentation(line);

            // Assert.
            containsReferenceInDocumentation.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow("<see />")]
        [DataRow("<see cref=\"Class Reference\"/>")]
        [DataRow("<cref=\"ClassReference\"/>")]
        [DataRow("<see cref=\"ClassReference\"")]
        [DataRow("see cref=\"ClassReference\"/>")]
        [DataRow("<see cref=\"\"/>")]
        [DataRow("///    <see />")]
        [DataRow("<see /> aaaaa")]
        [DataRow("///    <see /> aaaaa")]
        public void ContainsReferenceInDocumentation_NotReferenceInDocumentation_ReturnsFalse(string line)
        {
            // Arrange.
            ReferencesInDocumentationChecker referencesInDocumentationChecker = new ReferencesInDocumentationChecker();

            // Act.
            bool containsReferenceInDocumentation = referencesInDocumentationChecker
                .ContainsReferenceInDocumentation(line);

            // Assert.
            containsReferenceInDocumentation.Should().BeFalse();
        }
    }
}