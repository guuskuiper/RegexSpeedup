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
    public class WhitespaceBenchmark
    {
        private const string Spaces = " a  b   c     d     e      ";

        [Benchmark]
        public string RegexImplementation()
        {
            return RemoveAdditionalWhiteSpaceRegex.ReplaceWhiteSpaces(Spaces);
        }
        
        [Benchmark]
        public string CustomImplementation()
        {
            throw new NotImplementedException();
        }
    }
}
