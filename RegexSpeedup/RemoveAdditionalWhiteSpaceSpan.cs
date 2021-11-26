using System;

namespace RegexSpeedup
{
    public static class RemoveAdditionalWhiteSpaceSpan
    { 
        public static ReadOnlySpan<char> ReplaceWithSingleWhiteSpace(ReadOnlySpan<char> text)
        {
            char[] buffer = new char[text.Length];
            var span = new Span<char>(buffer);
            int bufferIndex = 0;

            bool started = false;
            int start = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if(started)
                {
                    if(IsWhiteSpace(text[i]))
                    {
                        started = false;
                        Add(start, i, text, span);
                    }
                }
                else
                {
                    if(!IsWhiteSpace(text[i]))
                    {
                        started = true;
                        start = i;
                    }
                }
            }

            if(started)
            {
                Add(start, text.Length, text, span);
            }

            void Add(int s, int e, ReadOnlySpan<char> t, Span<char> d)
            {
                if (bufferIndex > 0)
                {
                    d[bufferIndex] = ' ';
                    bufferIndex++;
                }

                int count = e - s;
                t.Slice(start, count).CopyTo(d.Slice(bufferIndex, count));
                bufferIndex += count;
            }

            return new ReadOnlySpan<char>(buffer, 0, bufferIndex);
        }

        public static Span<char> ReplaceWithSingleWhiteSpaceAllocFree(Span<char> text)
        {
            bool started = false;
            int bufferIndex = 0;
            int startIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (started)
                {
                    if (IsWhiteSpace(text[i]))
                    {
                        started = false;
                        Add(startIndex, i, text);
                    }
                }
                else
                {
                    if (!IsWhiteSpace(text[i]))
                    {
                        started = true;
                        startIndex = i;
                    }
                }
            }

            if (started)
            {
                Add(startIndex, text.Length, text);
            }

            void Add(int s, int e, Span<char> t)
            {
                int count = e - s;

                if (bufferIndex > 0)
                {
                    t[bufferIndex] = ' ';
                    bufferIndex++;
                }

                if (startIndex != bufferIndex)
                {
                    t.Slice(startIndex, count).CopyTo(t.Slice(bufferIndex, count));
                }
                bufferIndex += count;
            }

            return text.Slice(0, bufferIndex);
        }

        public static bool IsWhiteSpace(char c)
        {
            return c == ' ' || c == '\t';
        }
    }
}