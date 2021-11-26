using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegexSpeedup;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class DoubleWhitespaceBenchmark
    {
        private const string Spaces = " a  b   c     d     e      ";
        
        [Benchmark]
        public string DoubleUseWhitespaceString()
        {
            var res = RemoveAdditionalWhiteSpaceRegex.ReplaceWhiteSpaces(Spaces);
            return RemoveAdditionalWhiteSpaceRegex.ReplaceWhiteSpaces(res);
        }
        
        [Benchmark]
        public string DoubleUseWhitespaceStringReadOnlySpan()
        {
            var res = RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpace(Spaces.ToCharArray());
            return RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpace(res).ToString();
        }
        
        [Benchmark]
        public string DoubleUseWhitespaceStringSpan()
        {
            var res = RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(Spaces.ToCharArray());
            return RemoveAdditionalWhiteSpaceSpan.ReplaceWithSingleWhiteSpaceAllocFree(res).ToString();
        }
    }
}
