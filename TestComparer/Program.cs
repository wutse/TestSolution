using System.Buffers.Binary;

namespace TestComparer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Rule<int?> r = new Rule<int?>();

            //var result = r.IsMatch(8, 10);

            Dictionary<object, string> dic = new Dictionary<object, string>();

            string key1 = "AAA";
            string key2 = "AAA";

            dic.Add(key1, key1);


            if (dic.ContainsKey(key2))
            {
                Console.WriteLine(dic[key2]);
            }


            byte[] b1 = new byte[] { 98, 10, 70, 10 };

            int a1 = BinaryPrimitives.ReadInt32LittleEndian(b1);
            int a2 = BinaryPrimitives.ReadInt32BigEndian(b1);


            Console.ReadLine();
        }



    }

    public interface IRule<T>
        where T : IComparable<T>
    {
        public int IsMatch(T x, T y);
    }

    public class Rule<T> : IRule<T>
        where T : IComparable<T>
    {
        public int IsMatch(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}