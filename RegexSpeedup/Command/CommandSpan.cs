using System;

namespace RegexSpeedup.Command
{
    public static class CommandSpan
    {
        public static bool TryParseCommand(string text, out int lineNumber, out ReadOnlySpan<char> command, out ReadOnlySpan<char> parameters, out ReadOnlySpan<char> comment)
        {
            ReadOnlySpan<char> inputChar = text.AsSpan();
            
            if (TryParseLineNumber(inputChar, out lineNumber, out ReadOnlySpan<char> remaining))
            {
                return TryParseACommand(remaining, out command, out parameters, out comment);
            }
            
            parameters = ReadOnlySpan<char>.Empty;
            command = ReadOnlySpan<char>.Empty;
            comment = ReadOnlySpan<char>.Empty;

            return false;
        }

        private static bool TryParseLineNumber(ReadOnlySpan<char> inputChar, out int lineNumber, out ReadOnlySpan<char> remaining)
        {
            lineNumber = -1;
            remaining = inputChar;
            if (inputChar[0] != 'N') return false;

            int numberIndex = 1;
            while (char.IsDigit(inputChar[numberIndex]))
            {
                numberIndex++;
            }

            if (numberIndex == 1 || inputChar[numberIndex] != ' ') return false;
            
            var numberSlice = inputChar.Slice(1, numberIndex - 1);

            lineNumber = ParseInt(numberSlice);

            remaining = inputChar.Slice(numberIndex);
            
            return true;
        }

        private static int ParseInt(ReadOnlySpan<char> number)
        {
#if NET5_0_OR_GREATER
            return int.Parse(number);
#else
            return int.Parse(number.ToString());
#endif
        }

        private const string SET = "#set ";

        private static bool TryParseACommand(ReadOnlySpan<char> input, out ReadOnlySpan<char> command, out ReadOnlySpan<char> parameters, out ReadOnlySpan<char> remaining)
        {
            if(!TryFindSet(input, out int skip))
            {
                command = ReadOnlySpan<char>.Empty;
                parameters = ReadOnlySpan<char>.Empty;
                remaining = ReadOnlySpan<char>.Empty;
                
                return false;
            }

            var startSlice = SkipSpaces(input, skip);

            if (!TryParseCommandName(startSlice, out command, out ReadOnlySpan<char> paramSlice))
            {
                parameters = ReadOnlySpan<char>.Empty;
                remaining = ReadOnlySpan<char>.Empty;
                
                return false;
            }

            if (!TryEndParamIndex(paramSlice, out var onlyParams, out var afterParams))
            {
                parameters = ReadOnlySpan<char>.Empty;
                remaining = ReadOnlySpan<char>.Empty;
                
                return false;
            }

            if (!TryCheckParameters(onlyParams, out parameters))
            {
                remaining = ReadOnlySpan<char>.Empty;
                return false;
            }
            
            if (!TryParseComment(afterParams, out remaining))
            {
                return false;
            }

            return true;
        }

        private static bool TryParseComment(ReadOnlySpan<char> afterParams, out ReadOnlySpan<char> remaining)
        {
            var endIndex = afterParams.IndexOf('#');
            if (endIndex < 0)
            {
                remaining = ReadOnlySpan<char>.Empty;
                return false;
            }
            
            remaining = afterParams.Slice(endIndex + 1).TrimStart();
            return true;
        }

        private static bool TryEndParamIndex(ReadOnlySpan<char> paramSlice, out ReadOnlySpan<char> onlyParams, out ReadOnlySpan<char> afterParams)
        {
            var endParamIndex = paramSlice.IndexOf(')');

            onlyParams = paramSlice.Slice(0, endParamIndex);
            afterParams = paramSlice.Slice(endParamIndex + 1);
            return endParamIndex >= 0;
        }

        private static bool TryParseCommandName(ReadOnlySpan<char> slice, out ReadOnlySpan<char> command, out ReadOnlySpan<char> nextSlice)
        {
            int paramStartIndex = slice.IndexOf('(');
            if (paramStartIndex <= 0)
            {
                nextSlice = ReadOnlySpan<char>.Empty;
                command = ReadOnlySpan<char>.Empty;
                return false;
            }
            
            command = slice.Slice(0, paramStartIndex);
            nextSlice = slice.Slice(paramStartIndex + 1);
            return true;
        }

        private static bool TryCheckParameters(ReadOnlySpan<char> slice, out ReadOnlySpan<char> parameters)
        {
            bool correct = true;
            for (int i = 0; i < slice.Length; i++)
            {
                if (slice[i] == '#')
                {
                    correct = false;
                    break;
                }
            }
            
            parameters = slice.Trim();

            return correct;
        }

        private static ReadOnlySpan<char> SkipSpaces(ReadOnlySpan<char> input, int skip)
        {
            skip += SET.Length;

            while (input[skip] == ' ')
            {
                skip++;
            }

            return input.Slice(skip);
        }

        private static bool TryFindSet(ReadOnlySpan<char> input, out int skip)
        {
            skip = input.IndexOf(SET.AsSpan());
            return skip >= 0;
        }

        public static bool TryParseInt(ReadOnlySpan<char> number, out int value)
        {
            value = 0;
            number = number.Trim();

            bool negative = false;
            int index = 0;
            if (number[index] == '-')
            {
                index++;
                negative = true;
            }

            for (; index < number.Length; index++)
            {
                char current = number[index];
                if (current < '0' || current > '9')
                {
                    return false;
                }
                value = value * 10 + current - '0';
            }

            if (negative)
            {
                value = -value;
            }

            return true;
        }
    }
}