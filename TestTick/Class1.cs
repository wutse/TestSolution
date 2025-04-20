using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTick
{
    public delegate void TestHandler();

    public interface ITest
    { }

    public class Test1 : ITest { }
    public class Test2 : ITest { }

    internal class Class1
    {
        public event TestHandler TestE1;
        public event TestHandler TestE2;

        public void Test()
        {
            Task t1 = Task.Run(() =>
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    long j = 0;
                    while (true)
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            sw.Restart();
                            TestE1?.Invoke();
                            sw.Stop();
                        }
                        j += sw.ElapsedTicks;
                        Console.WriteLine($"Task 1 = {sw.ElapsedTicks}, {j}");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            });

            Task t2 = Task.Run(() =>
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    long j = 0;
                    while (true)
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            sw.Restart();
                            TestE1?.Invoke();
                            sw.Stop();
                        }

                        j += sw.ElapsedTicks;
                        Console.WriteLine($"Task 2 = {sw.ElapsedTicks}, {j}");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            });

            Task.WhenAll(new Task[] { t1, t2 });
        }
    }
}
