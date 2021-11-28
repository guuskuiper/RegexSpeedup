using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegexSpeedup;
using RegexSpeedup.Whitespace;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class WhitespaceBenchmark
    {
        private const string Spaces2 = " a  b   c     d     e      ";
        private const string Spaces = " aaaaaaaaaaaaaaaaaa  bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb   cccccccc    deeeeeeeeeeee     eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee      ";

        [Benchmark]
        public string RegexImplementation()
        {
            return RemoveAdditionalWhiteSpaceRegex.ReplaceWhiteSpaces(Spaces);
        }
        
        [Benchmark]
        public string CustomImplementation()
        {
            return CustomRemoveAdditionalWhiteSpaceRegex.RemoveAdditionalWhiteSpace(Spaces);
        }
        
        [Benchmark]
        public string UseWhitespaceStringReadOnlySpan()
        {
            return RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpace(Spaces.AsSpan()).ToString();
        }
        
        [Benchmark]
        public string UseWhitespaceStringSpan()
        {
            var chars = Spaces.ToCharArray();
            int count = RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(chars);
            return new string(chars, 0, count);
        }

        [Benchmark]
        public string UseCharArray()
        {
            return CharArray.RemoveAdditionalWhiteSpace(Spaces);
        }
        
        [Benchmark]
        public string SpanStackAlloc()
        {
            unsafe
            {
                var chars = stackalloc char[Spaces.Length];
                Span<char> span = new Span<char>(chars, Spaces.Length);
                Spaces.AsSpan().CopyTo(span);
                int count = RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(span);
                return new string(chars, 0, count);
            }
        }

        [Benchmark]
        public string ReadOnlySpanStackAlloc()
        {
            return RemoveAdditionalWhiteSpaceSpan.ReadOnlySpanBuffer(Spaces.AsSpan());
        }
    }
}
