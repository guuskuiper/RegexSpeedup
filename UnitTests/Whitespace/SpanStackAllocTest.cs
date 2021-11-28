using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Whitespace;

namespace UnitTests.Whitespace
{
    [TestClass]
    public class SpanStackAllocTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            unsafe
            {
                var chars = stackalloc char[input.Length];
                Span<char> span = new Span<char>(chars, input.Length);
                input.AsSpan().CopyTo(span);
                int count = RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(span);
                return new string(chars, 0, count);
            }
        }
    }
}