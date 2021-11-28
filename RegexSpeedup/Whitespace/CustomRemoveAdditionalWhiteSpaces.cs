namespace RegexSpeedup.Whitespace
{
    public static class CustomRemoveAdditionalWhiteSpaceRegex
    {
        public static string RemoveAdditionalWhiteSpace(string input)
        {
            unsafe
            {
                fixed (char* input_start = input)
                {
                    var newChars = new char[input.Length];

                    var skipped = 0;
                    var l = input.Length;
                    char* p = input_start;
                    var lastNonSpace = -1;
                    var lastWasSpace = true;
                    for (var i = 0; i < input.Length; i++, p++)
                    {
                        var isSpace = *p == ' ' || *p == '\t';
                        if (isSpace && lastWasSpace)
                        {
                            skipped++;
                        }
                        else
                        {
                            var pos = i - skipped;
                            if (!isSpace)
                            {
                                lastNonSpace = pos;
                            }
                            newChars[pos] = *p != '\t' ? *p : ' ';
                        }

                        lastWasSpace = isSpace;
                    }

                    return new string(newChars, 0, lastNonSpace + 1);
                }
            }
        }
    }
}
