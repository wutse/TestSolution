using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TestLogString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var summary = BenchmarkRunner.Run<TestLogStringWriter>();

            //TestLogStringWriter test = new TestLogStringWriter();

            //test.InterWriter("AAA");
            //test.StructWriter("AAA");
            //test.IfWriter("AAA");
            //test.LogHelperWriter("AAA");

            Console.ReadLine();
        }
    }

    [MemoryDiagnoser, MinColumn, MaxColumn]
    public class TestLogStringWriter
    {
        private static ILogger _logger1;

        private static ILogger _logger2;


        static TestLogStringWriter()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.Services.AddSingleton<ILoggerProvider, InMemoryProvider>());
            _logger1 = loggerFactory.CreateLogger(nameof(FileLogger));

            _logger2 = new FileLogger();
        }

        [Benchmark]
        public void InterWriter()
        {
            string msg = "AAA";
            _logger1.LogDebug($"InterWriter:{msg}");
        }
        [Benchmark]
        public void StructWriter()
        {
            string msg = "AAA";
            _logger1.LogDebug("StructWriter:{msg}", msg);
        }

        [Benchmark]
        public void IfWriter()
        {
            string msg = "AAA";
            if (_logger1.IsEnabled(LogLevel.Trace))
            {
                string s = DateTime.Now.ToString();
            }
        }

        [Benchmark]
        public void LogHelperNoIfWriter1()
        {
            string msg = "AAA";
            _logger1.LogMsg(msg);
        }

        [Benchmark]
        public void LogHelperWriter1()
        {
            string msg = "AAA";
            if (_logger1.IsEnabled(LogLevel.Debug))
            {
                _logger1.LogMsg(msg);
            }
        }

        [Benchmark]
        public void LogHelperNoIfWriter2()
        {
            string msg = "AAA";
            _logger2.LogMsg(msg);
        }

        [Benchmark]
        public void LogHelperWriter2()
        {
            string msg = "AAA";
            if (_logger2.IsEnabled(LogLevel.Debug))
            {
                _logger2.LogMsg(msg);
            }
        }
    }

    public static partial class LogHelper
    {
        [LoggerMessage(EventId = 1, Level = LogLevel.Debug, Message = "LogHelper:{msg}")]
        public static partial void LogMsg(this ILogger logger, string msg);
    }
    public class InMemoryProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new FileLogger();

        public void Dispose()
        {
        }
    }

    public class FileLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            using (StreamWriter stream = new StreamWriter("FileLogger", true))
            {
                stream.WriteLine(formatter.Invoke(state, null));
            }
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => default;
    }
}