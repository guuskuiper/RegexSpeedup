﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup;

namespace UnitTests
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