using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Whitespace;

namespace UnitTests.Whitespace
{
    [TestClass]
    public class ReadOnlySpanStackAllocTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            return RemoveAdditionalWhiteSpaceSpan.ReadOnlySpanBuffer(input.AsSpan());
        }
    }
}