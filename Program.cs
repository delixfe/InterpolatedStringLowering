using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace InterpolatedStringLowering
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Runtime[] runtimes = new Runtime[]{CoreRuntime.Core31, CoreRuntime.Core50, CoreRuntime.Core60, CoreRuntime.Core70, CoreRuntime.Core80, CoreRuntime.Core90};
            Job[] jobs = runtimes.Select(r => Job.ShortRun.WithRuntime(r).WithBaseline(r.Name == CoreRuntime.Core80.Name)).ToArray();
            var config = ManualConfig.CreateMinimumViable().AddJob(jobs).AddExporter(MarkdownExporter.GitHub);
            _ = BenchmarkRunner.Run<Benchmarks>(config, args);

            // Use this to select benchmarks from the console:
            // var summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}
