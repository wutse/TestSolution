using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Buffers;
using System.Collections.Concurrent;

namespace TestMemoryPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TestClass testClass = new TestClass();
            testClass.TestMethod1();

            Console.ReadLine();
        }
    }


    public class TestObject
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }

    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class TestClass
    {
        [Benchmark]
        public void TestMethod1()
        {
            Random random = new Random();
            BlockingCollection<TestObject> _blockingCollection = new BlockingCollection<TestObject>();

            int index = 0;

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    for (int i = 0; i < 2048; i++)
                    {
                        TestObject obj = new TestObject
                        {
                            Index = ++index,
                            Name = "John Doe",
                            Age = 30,
                            Address = "123 Main St"
                        };

                        _blockingCollection.Add(obj);

                        //await Task.Delay(random.Next(50, 70));
                    }

                    await Task.Delay(2500); // Simulate some delay between writes
                }
            });

            _ = Task.Run(async () =>
            {
                foreach (var obj in _blockingCollection.GetConsumingEnumerable())
                {
                    // Simulate processing the object
                    Console.WriteLine($"Processing object with Index: {obj.Index}, Name: {obj.Name}, Age: {obj.Age}, Address: {obj.Address}");
                    // Simulate some processing time
                    //await Task.Delay(random.Next(50, 70));
                }
            });
        }

        public void TestMethod2()
        {
            MemoryPool<TestObject> memoryPool = MemoryPool<TestObject>.Shared;
            BlockingCollection<TestObject> _blockingCollection = new BlockingCollection<TestObject>();

            int index = 0;

            _ = Task.Run(() =>
            {
                while (true)
                {
                    using (var rentedBuffer = memoryPool.Rent(2048))
                    {
                        Span<TestObject> rentObjs = rentedBuffer.Memory.Span;

                        for (int i = 0; i < 2048; i++)
                        {
                            rentObjs[i] = new TestObject
                            {
                                Index = ++index,
                                Name = "John Doe",
                                Age = 30,
                                Address = "123 Main St"
                            };

                            _blockingCollection.Add(rentObjs[i]);
                        }
                    }

                    Thread.Sleep(2500); // Simulate some delay between writes
                }
            });

            _ = Task.Run(() =>
            {
                foreach (var obj in _blockingCollection.GetConsumingEnumerable())
                {
                    // Simulate processing the object
                    Console.WriteLine($"Processing object with Index: {obj.Index}, Name: {obj.Name}, Age: {obj.Age}, Address: {obj.Address}");

                    obj = null;
                }
            });
        }
    }
}
