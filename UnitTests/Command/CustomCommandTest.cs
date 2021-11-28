using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Command;

//[TestClass]
public class CustomCommandTest : BaseCommandTest
{
    protected override bool TryParseCommand(string input, out int lineNumber, out string command, out string parameters, out string comment)
    {
        throw new System.NotImplementedException();
    }
}