using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup;

namespace UnitTests
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