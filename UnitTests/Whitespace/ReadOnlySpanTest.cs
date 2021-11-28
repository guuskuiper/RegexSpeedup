﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Whitespace;

namespace UnitTests.Whitespace
{
    [TestClass]
    public class ReadOnlySpanTest : RemoveWhiteSpaceBaseTest
    {
        protected override string RemoveAdditionalWhiteSpace(string input)
        {
            return RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpace(input).ToString();
        }
    }
}