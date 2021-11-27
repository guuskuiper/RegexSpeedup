using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Command;

namespace UnitTests.Command;

[TestClass]
public class RegexCommandTest : BaseCommandTest
{
    protected override bool TryParseCommand(string input, out int lineNumber, out string command, out string parameters, out string comment)
    {
        return CommandRegex.TryParseCommand(input, out lineNumber, out command, out parameters, out comment);
    }
}