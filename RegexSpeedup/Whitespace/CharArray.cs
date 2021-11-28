namespace RegexSpeedup.Whitespace
{
    public static class CharArray
    {
        public static string RemoveAdditionalWhiteSpace(string input)
        {
            char[] text = input.ToCharArray();
            int textIndex = 0;
            bool firstChar = true;
            bool prevWasSpace = false;
            for (int i = 0; i < text.Length; i++)
            {
                if(!char.IsWhiteSpace(text[i]))
                {
                    if (!firstChar & prevWasSpace)
                    {
                        text[textIndex++] = ' ';
                    }
                    text[textIndex++] = text[i];

                    firstChar = false;
                    prevWasSpace = false;
                }
                else
                {
                    prevWasSpace = true;
                }
            }

            return new string(text, 0, textIndex);
        }
    }
}
