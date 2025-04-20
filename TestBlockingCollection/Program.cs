using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TestBlockingCollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            bcTask = Task.Run(new Action(BCProcess));

            cqTask = Task.Run(new Action(CQProcess));

            int index = 0;
            while (true)
            {
                ++index;
                bc.Add(index.ToString());
                cq.Enqueue(index.ToString());
                autoReset.Set();
                Task.Delay(3000).Wait();
            }

            Console.ReadLine();
        }

        private static Task bcTask;
        private static BlockingCollection<string> bc = new BlockingCollection<string>();

        private static async void BCProcess()
        {
            try
            {
                foreach (var s in bc.GetConsumingEnumerable())
                {
                    Console.WriteLine($"BC {s}");
                    //await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private static Task cqTask;
        private static ConcurrentQueue<string> cq = new ConcurrentQueue<string>();
        private static AutoResetEvent autoReset = new AutoResetEvent(false);

        private static async void CQProcess()
        {
            try
            {
                while (true)
                {
                    if (cq.TryDequeue(out string s))
                    {
                        Console.WriteLine($"CQ {s}");
                        //await Task.Delay(1000);
                    }
                    else
                    {
                        Console.WriteLine($"CQ NA");
                        autoReset.WaitOne();
                        //await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

    }
}