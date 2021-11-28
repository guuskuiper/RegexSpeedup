using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexSpeedup.Command;

namespace UnitTests.Command;

[TestClass]
public class SpanCommandTest : BaseCommandTest
{
    protected override bool TryParseCommand(string input, out int lineNumber, out string command, out string parameters, out string comment)
    {
        bool result = CommandSpan.TryParseCommand(input, out lineNumber, out ReadOnlySpan<char> commandSpan, out ReadOnlySpan<char> parametersSpan, out ReadOnlySpan<char> commentSpan);
        command = commandSpan.ToString();
        parameters = parametersSpan.ToString();
        comment = commentSpan.ToString();
        return result;
    }
}