
namespace TestTimer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TestClass test = new TestClass();
            test.Start();

            //_ = Task.Run(() =>
            //{
            //    Thread.Sleep(10000);
            //});

            Console.ReadLine();

        }


    }

    public class TestClass
    {
        public List<Timer> Timers { get; set; } = new List<Timer>();
        public void Start()
        {
            ThreadPool.GetAvailableThreads(out int worker, out int io);
            Console.WriteLine($"Current Thread[{Thread.CurrentThread.ManagedThreadId}], Worker[{worker}], IO[{io}].");

            for (int i = 0; i < 3; i++)
            {
                var timer = new Timer(TimerHandler);
                timer.Change(0, 1000);

                Timers.Add(timer);
            }
        }

        private void TimerHandler(object? state)
        {
            ThreadPool.GetAvailableThreads(out int worker, out int io);
            Console.WriteLine($"Current Thread[{Thread.CurrentThread.ManagedThreadId}], Worker[{worker}], IO[{io}].");
        }
    }
}
