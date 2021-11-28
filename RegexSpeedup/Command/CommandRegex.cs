using System.Text.RegularExpressions;

namespace RegexSpeedup.Command
{
    public static class CommandRegex
    {
        private static readonly Regex _commandRegex = new Regex(
            @"N(?<lineNum>([0-9]+))" + 
            @"\s*(\s*#set\s*(?<command>([a-z,A-Z][a-z,A-Z,_,0-9]*))" + 
            @"\s*\(\s*(?<params>(((\d*(\.\d+)?))R?(\s*;\s*((\d*(\.\d+)?))R?)*)*)\s*\)\s*#)" + 
            @"\s*(?<comment>((;.*$)|(\([^\(^\)]*?\) *))*)$",
            RegexOptions.Compiled);

        public static bool TryParseCommand(string text, out int lineNumber, out string command, out string parameters, out string comment)
        {
            var match = _commandRegex.Match(text); 
            if (match.Success)
            {
                lineNumber = int.Parse(match.Groups["lineNum"].Value);
                command = match.Groups["command"].Value;
                parameters = match.Groups["params"].Value;
                comment = match.Groups["comment"].Value;
            }
            else
            {
                lineNumber = -1;
                command = "";
                parameters = "";
                comment = "";
            }

            return match.Success;
        }
    }
}