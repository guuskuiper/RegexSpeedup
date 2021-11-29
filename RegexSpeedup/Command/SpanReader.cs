using System;

namespace RegexSpeedup.Command
{

    public class ParseException : Exception
    {
    }

    public ref struct SpanReader
    {
        private ReadOnlySpan<char> _buffer;

        public SpanReader(ReadOnlySpan<char> buffer)
        {
            _buffer = buffer;
        }

        public int ParseInt()
        {
            int index = _buffer.IndexOf(' ');
            if (index < 0)
            {
                throw new ParseException();
            }

            if (!TryParseInt(_buffer.Slice(0, index), out int value))
            {
                throw new ParseException();
            }

            Advance(index);
            return value;
        }

        private void Advance(int index)
        {
            _buffer = _buffer.Slice(index);
        }

        public void SkipWhitespaces()
        {
            int index = -1;
            for (int i = 0; i < _buffer.Length; i++)
            {
                if (_buffer[i] != ' ')
                {
                    index = i - 1;
                    break;
                }
            }
            
            if (index >= 0)
            {
                Advance(index + 1);
            }
        }

        public void ParseExpect(ReadOnlySpan<char> expected)
        {
            if (!_buffer.StartsWith(expected))
            {
                throw new ParseException();
            }

            Advance(expected.Length);
        }
        
        public void ParseExpect(char expected)
        {
            if (_buffer.IsEmpty || _buffer[0] != expected)
            {
                throw new ParseException();
            }

            Advance(1);
        }

        public ReadOnlySpan<char> ParseText()
        {
            int index = -1;
            for (int i = 0; i < _buffer.Length; i++)
            {
                if (!char.IsLetter(_buffer[i]) && (i > 0 && !char.IsDigit(_buffer[i])))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                throw new ParseException();
            }

            ReadOnlySpan<char> text = _buffer.Slice(0, index);
            Advance(index);
            return text;
        }

        public ReadOnlySpan<char> ParseUntil(char until)
        {
            var index = _buffer.IndexOf(until);
            if (index < 0)
            {
                throw new ParseException();
            }

            var text = _buffer.Slice(0, index);
            Advance(index + 1);
            return text;
        }

        public ReadOnlySpan<char> GetCurrent()
        {
            return _buffer;
        }

        private static bool TryParseInt(ReadOnlySpan<char> number, out int value)
        {
#if NET5_0_OR_GREATER
            return int.TryParse(number, out value);
#else
            return int.TryParse(number.ToString(), out value);
#endif
        }
    }
}