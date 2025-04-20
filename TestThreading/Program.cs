using System.Security.Cryptography;

namespace TestThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TestClass testClass = new TestClass()
            {
                PropertyA = "0",
                PropertyB = "A",
            };

            _ = Task.Run(() => 
            {
                testClass.BeginUpdate();

                for(int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Task 1 update {i}.");
                    Console.WriteLine($"Task 1 {testClass.PropertyA += i}");
                    Console.WriteLine($"Task 1 {testClass.PropertyB += i}");
                    SpinWait.SpinUntil(() => false, 5000);
                }

                testClass.EndUpdate();
            });

            _ = Task.Run(() => 
            {
                int i = 0;
                while (true)
                {
                    SpinWait.SpinUntil(() => false, 1000);
                    Console.WriteLine($"Task 2 wait {++i}s.");

                    testClass.PropertyA = "============";
                    testClass.PropertyB = "++++++++++++";

                    Console.WriteLine($"Task 2 {testClass.PropertyA}");
                    Console.WriteLine($"Task 2 {testClass.PropertyB}");
                }
            });


            Console.ReadLine();
        }
    }

    public class TestClass
    {
        private object _lockObj = new object();

        public string PropertyA { get; set; }
        public string PropertyB { get; set; }

        public void BeginUpdate()
        {
            Monitor.Enter(_lockObj);
        }

        public void EndUpdate()
        {
            Monitor.Exit(_lockObj);
        }
    }
}