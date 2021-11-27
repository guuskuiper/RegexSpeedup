using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Whitespace
{
    public abstract class RemoveWhiteSpaceBaseTest
    {
        [TestMethod] public void Empty() => Check("", "");
        [TestMethod] public void Space() => Check("", " ");
        [TestMethod] public void Spaces() => Check("", "   ");

        [TestMethod] public void Tab() => Check("", "\t");
        [TestMethod] public void TabSpace() => Check("", "\t ");

        [TestMethod] public void SpaceChar() => Check("a", " a");
        [TestMethod] public void SpacesChar() => Check("a", "  a");
        [TestMethod] public void TabChar() => Check("a", "\ta");

        [TestMethod] public void CharSpace() => Check("a", "a ");
        [TestMethod] public void CharSpaces() => Check("a", "a  ");
        [TestMethod] public void SpacesCharSpaces() => Check("a", "  a   ");
        [TestMethod] public void CharSpacesChar() => Check("a b", "a     b");
        [TestMethod] public void Many() => Check("a b c d", " a   b    c  \t\t      d   ");
        [TestMethod] public void OverlappingDoubleSpace() => Check("abcd efghij", "abcd  efghij");

        private void Check(string expected, string input)
        {
            var output = RemoveAdditionalWhiteSpace(input);
            Assert.AreEqual(expected, output);
        }

        protected abstract string RemoveAdditionalWhiteSpace(string input);
    }
}