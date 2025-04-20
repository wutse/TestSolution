using System.Diagnostics;

namespace TestComposition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassA classA = new ClassA();
            classA.PropertyA = "AAA";
            classA.PropertyB = "BBB";
            classA.PropertyC = 0;
            classA.PropertyD = 0m;
            classA.PropertyE = 0m;

            ClassB classB = new ClassB();
            classB.PropertyA = "AAA";
            classB.SubClassB1.PropertyB = "BBB";
            classB.SubClassB1.PropertyC = 0;
            classB.SubClassB2.PropertyD = 0m;
            classB.SubClassB2.PropertyE = 0m;

            long tick1 = 0;
            Task task1 = Task.Run(() => 
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                decimal index = 0m;
                for (int i = 0; i < 1; i++)
                {
                    index += i;
                    classA.PropertyA = "AAA" + i;
                    classA.PropertyB = "BBB" + i;
                    classA.PropertyC = i;
                    classA.PropertyD = index;
                    classA.PropertyE += (index + i);

                    classA.Reset();
                }

                sw.Stop();
                tick1 = sw.ElapsedTicks;
                Console.WriteLine($"T1 = {tick1}");
            });

            long tick2 = 0;
            Task task2 = Task.Run(() => 
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                decimal index = 0m;
                for (int i = 0; i < 1; i++)
                {
                    index += i;
                    classB.PropertyA = "AAA" + i;
                    classB.SubClassB1.PropertyB = "BBB" + i;
                    classB.SubClassB1.PropertyC = i;
                    classB.SubClassB2.PropertyD = index;
                    classB.SubClassB2.PropertyE = (index + i);

                    classB.Reset();
                }

                sw.Stop();
                tick2 = sw.ElapsedTicks;
                Console.WriteLine($"T2 = {tick2}");
            });

            Task.WaitAll(task1, task2);

            long μs = (tick2 - tick1) / TimeSpan.TicksPerMicrosecond;
            Console.WriteLine($"total microseconds = {μs}");

            Console.ReadLine();

        }
    }

    public class ClassA
    {
        public string PropertyA { get; set; }
        public string PropertyB { get; set; }
        public int PropertyC { get; set; }
        public decimal PropertyD { get; set; }
        public decimal? PropertyE { get; set; }

        public void Reset()
        {
            this.PropertyA = "AAA";
            this.PropertyB = "BBB";
            this.PropertyC = 0;
            this.PropertyD = 0m;
            this.PropertyE = 0m;
        }
    }


    public class ClassB
    {
        public string PropertyA { get; set; }
        public SubClassB1 SubClassB1 { get; set; } = new SubClassB1();
        public SubClassB2 SubClassB2 { get; set; } = new SubClassB2();

        public void Reset()
        {
            this.PropertyA = "AAA";
            this.SubClassB1.PropertyB = "BBB";
            this.SubClassB1.PropertyC = 0;
            this.SubClassB2.PropertyD = 0m;
            this.SubClassB2.PropertyE = 0m;
        }
    }

    public class SubClassB1
    {
        public string PropertyB { get; set; }
        public int PropertyC { get; set; }

    }
    public class SubClassB2
    {
        public decimal PropertyD { get; set; }
        public decimal? PropertyE { get; set; }

    }
}