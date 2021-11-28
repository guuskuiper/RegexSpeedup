using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manualConfig = ManualConfig.CreateMinimumViable()
                .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60))
                .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50))
                .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48));
            
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args, manualConfig);
        }
    }
}
