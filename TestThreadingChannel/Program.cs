using System.Threading.Channels;

namespace TestThreadingChannel
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            await TestMethod2();

            Console.ReadKey();
        }

        public static async Task TestMethod()
        {
            var channel = Channel.CreateUnbounded<string>();

            long count = 1;
            _ = Task.Run(async () =>
            {
                Random r = new Random(DateTime.Now.Microsecond);
                while (true)
                {
                    string msg = $"{Thread.CurrentThread.ManagedThreadId} - {count++}";
                    Console.WriteLine($"Write : {msg}");
                    channel.Writer.TryWrite(msg);

                    await Task.Delay(r.Next(1000));
                }
            });

            //_ = Task.Run(async () =>
            //{
            //    Random r = new Random(DateTime.Now.Microsecond);
            //    while (true)
            //    {
            //        string msg = $"{Thread.CurrentThread.ManagedThreadId} - {count++}";
            //        Console.WriteLine($"Write : {msg}");
            //        channel.Writer.TryWrite(msg);

            //        await Task.Delay(r.Next(1000));
            //    }
            //});

            _ = Task.Run(async () =>
            {
                Random r = new Random(DateTime.Now.Microsecond);
                var reader = channel.Reader;
                while (await reader.WaitToReadAsync())
                {
                    if (reader.TryRead(out var msg))
                    {
                        Console.WriteLine($"Read : {msg}");
                    }


                    await Task.Delay(r.Next(100));
                }

                Console.WriteLine($"CONSUMER : Completed");
            });
        }

        public static async Task TestMethod2()
        {
            var channel = Channel.CreateUnbounded<string>();

            var producer1 = new Producer(channel.Writer, 1, 0);
            var consumer1 = new Consumer(channel.Reader, 1, 0);

            Task consumerTask1 = consumer1.ConsumeData();
            Task producerTask1 = producer1.BeginProducing();

            await producerTask1.ContinueWith(_ => channel.Writer.Complete());

            await consumerTask1;

        }
    }


    public class Producer
    {
        private readonly ChannelWriter<string> _writer;
        private readonly int _identifier;
        private readonly int _delay;

        public Producer(ChannelWriter<string> writer, int identifier, int delay)
        {
            _writer = writer;
            _identifier = identifier;
            _delay = delay;
        }

        public async Task BeginProducing()
        {
            Console.WriteLine($"PRODUCER ({_identifier}): Starting");

            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(_delay);

                var msg = $"P{_identifier} - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}";

                Console.WriteLine($"PRODUCER ({_identifier}): Creating {msg}");

                await _writer.WriteAsync(msg);
            }

            Console.WriteLine($"PRODUCER ({_identifier}): Completed");
        }
    }

    public class Consumer
    {
        private readonly ChannelReader<string> _reader;
        private readonly int _identifier;
        private readonly int _delay;

        public Consumer(ChannelReader<string> reader, int identifier, int delay)
        {
            _reader = reader;
            _identifier = identifier;
            _delay = delay;
        }

        public async Task ConsumeData()
        {
            Console.WriteLine($"CONSUMER ({_identifier}): Starting");

            while (await _reader.WaitToReadAsync())
            {
                if (_reader.TryRead(out var timeString))
                {
                    await Task.Delay(_delay);

                    Console.WriteLine($"CONSUMER ({_identifier}): Consuming {timeString}- {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
                }
            }

            Console.WriteLine($"CONSUMER ({_identifier}): Completed");
        }
    }
}