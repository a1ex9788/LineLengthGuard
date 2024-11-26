using FluentAssertions;
using LineLengthGuard.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineLengthGuard.Tests.UnitTests.Logic
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
        [DataRow("<cref=\"Reference\"/>")]
        [DataRow("<see cref=\"Reference\"")]
        [DataRow("see cref=\"Reference\"/>")]
        [DataRow("<see cref=\"\"/>")]
        [DataRow("///    <see />")]
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