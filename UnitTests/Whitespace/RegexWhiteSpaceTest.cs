using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Whitespace;

namespace UnitTests.Whitespace
{
    [TestClass]
    public class RemoveAdditionalWhiteSpaceTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            return RemoveAdditionalWhiteSpaceRegex.ReplaceWhiteSpaces(input);
        }
    }
}