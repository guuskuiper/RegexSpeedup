using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup;

namespace UnitTests
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