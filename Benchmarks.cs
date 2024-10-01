using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace InterpolatedStringLowering
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(exportDiff: true, printSource: true, exportCombinedDisassemblyReport: true)]
    public class Benchmarks
    {
        const string FieldName = "FieldName";

        readonly ILogger logger;
        readonly string value;

        public Benchmarks()
        {
            logger = NullLogger.Instance;
            value = Guid.NewGuid().ToString();
        }

        [Benchmark(Baseline = true)]
        public void StringLiteral()
        {
            logger.Log(LogLevel.Information, "Benchmarks FieldName:{FieldName}", value);
        }

        [Benchmark]
        public void FalsePositive()
        {
            logger.Log(LogLevel.Information, $"{nameof(Benchmarks)} " + //
                                             $"{FieldName}:{{{FieldName}}}", value);
        }

        [Benchmark]
        public void CorrectNegative()
        {
            logger.Log(LogLevel.Information, $"{nameof(Benchmarks)} {FieldName}:{{{FieldName}}}", value);
        }

        [Benchmark]
        public void CorrectPositive()
        {
            logger.Log(LogLevel.Information, $"Benchmarks FieldName:{value}");
        }
    }
}
