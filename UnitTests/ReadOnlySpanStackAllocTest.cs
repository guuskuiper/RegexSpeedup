using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup;

namespace UnitTests
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