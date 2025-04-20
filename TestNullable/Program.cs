using System.Drawing;
using System.Runtime.InteropServices;

namespace TestNullable
{
    internal class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public class TestSize1
        {
            public int? A { get; set; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TestSize2
        {
            public int A { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine($"1 >> {GC.GetTotalMemory(true)}");
            //unsafe
            //{
            //    Console.WriteLine(sizeof(int));
            //    Console.WriteLine(sizeof(int?));
            //    Console.WriteLine(sizeof(long));
            //    Console.WriteLine(sizeof(long?));
            //    Console.WriteLine(sizeof(decimal));
            //    Console.WriteLine(sizeof(decimal?));
            //}

            //Console.WriteLine(Marshal.SizeOf(typeof(TestSize1)));
            //Console.WriteLine(Marshal.SizeOf(typeof(TestSize2)));
            List<StockInfo> ss = new List<StockInfo>();

            for(int i = 0; i <100; i++)
            {
                TWStockInfo info = new TWStockInfo(Market.TW, "AA", "AAA");
            }

            Console.WriteLine($"2 >> {GC.GetTotalMemory(true)}");

            Console.Read();
        }
    }
}