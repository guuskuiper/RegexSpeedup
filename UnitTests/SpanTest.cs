using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup;

namespace UnitTests
{
    [TestClass]
    public class SpanTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            var chars = input.ToCharArray();
            int count = RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(chars);
            return new string(chars, 0, count);
        }
    }
}