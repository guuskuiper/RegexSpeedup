using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Whitespace;

namespace UnitTests.Whitespace
{
    [TestClass]
    public class CustomRemoveWhiteSpaceTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            return CustomRemoveAdditionalWhiteSpaceRegex.RemoveAdditionalWhiteSpace(input);
        }
    }
}