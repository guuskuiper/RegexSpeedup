using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegexSpeedup;
using RegexSpeedup.Command;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class CommandBenchmark
    {
        private const string Line = "N123450  #set AcommandName( 1 ; 3.14  ; R4)# ; Sets a command value";

        [Benchmark]
        public bool RegexImplementation()
        {
            return CommandRegex.TryParseCommand(Line, out int lineNumber, out string command, out string parameters, out string comment);
        }
        
        //[Benchmark]
        public bool CustomImplementation()
        {
            throw new NotImplementedException();
        }
    }
}
