using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CustomRemoveWhiteSpaceTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            return RegexSpeedup.CustomRemoveAdditionalWhiteSpaceRegex.RemoveAdditionalWhiteSpace(input);
        }
    }
}