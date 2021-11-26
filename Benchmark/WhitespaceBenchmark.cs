﻿using BenchmarkDotNet.Attributes;
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
            return RemoveAdditionalWhiteSpaceSpan
                .ReplaceWithSingleWhiteSpaceAllocFree(Spaces.ToCharArray()).ToString();
        }

        [Benchmark]
        public string UseCharArray()
        {
            return CharArray.RemoveAdditionalWhiteSpace(Spaces);
        }
    }
}
