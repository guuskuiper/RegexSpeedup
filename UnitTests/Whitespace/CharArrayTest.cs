using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup;

namespace UnitTests.Whitespace
{
    [TestClass]
    public class CharArrayTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            return CharArray.RemoveAdditionalWhiteSpace(input);
        }
    }
}