using System;

namespace RegexSpeedup.Command
{
    public static class CommandSpanReader
    {
        public static bool TryParseCommand(string text, out int lineNumber, out ReadOnlySpan<char> command,
            out ReadOnlySpan<char> parameters, out ReadOnlySpan<char> comment)
        {
            var reader = new SpanReader(text.AsSpan());
            
            if (!TryParseLineNumber(ref reader, out lineNumber))
            {
                parameters = ReadOnlySpan<char>.Empty;
                command = ReadOnlySpan<char>.Empty;
                comment = ReadOnlySpan<char>.Empty;

                return false;
            }

            if (!TryParseACommand(ref reader, out command, out parameters))
            {
                comment = ReadOnlySpan<char>.Empty;
                
                return false;
            }

            ParseComment(ref reader, out comment);

            return true;
        }



        private static bool TryParseLineNumber(ref SpanReader reader, out int lineNumber)
        {
            try
            {
                reader.ParseExpect('N');
                lineNumber = reader.ParseInt();
                reader.SkipWhitespaces();
            }
            catch (ParseException)
            {
                lineNumber = -1;
                return false;
            }
            
            return lineNumber >= 0;
        }

        private static bool TryParseACommand(ref SpanReader reader, out ReadOnlySpan<char> command,
            out ReadOnlySpan<char> parameters)
        {
            try
            {
                reader.ParseExpect("#set".AsSpan());
                reader.SkipWhitespaces();
                command = reader.ParseText();
                if (command.IsEmpty)
                {
                    parameters = ReadOnlySpan<char>.Empty;
                    return false;
                }
                reader.ParseExpect('(');
                parameters = reader.ParseUntil(')').Trim();
                if (parameters.Contains("#".AsSpan(), StringComparison.InvariantCulture))
                {
                    return false;
                }
                reader.SkipWhitespaces();
                reader.ParseExpect('#');
                return true;
            }
            catch (ParseException e)
            {
                command = ReadOnlySpan<char>.Empty;
                parameters = ReadOnlySpan<char>.Empty;
                return false;
            }
        }
        
        private static void ParseComment(ref SpanReader reader, out ReadOnlySpan<char> comment)
        {
            comment = reader.GetCurrent().TrimStart();
        }
    }
}