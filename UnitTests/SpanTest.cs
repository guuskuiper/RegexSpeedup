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
            return RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(new Span<char>(input.ToCharArray())).ToString();
        }

        private static readonly string spaces = " a  b   c     d     e      ";
        private static readonly char[] spacesArray = spaces.ToCharArray();
        
        [TestMethod]
        public void ChangesArray()
        {
            RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(spacesArray).ToString();
            string fromArray = new string(spacesArray);
            Assert.AreNotEqual(spaces, fromArray);
        }
    }
}