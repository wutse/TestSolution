using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

#if DEBUG
using Microsoft.VSDiagnostics;
#endif

namespace TestSpan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //TestClass testClass = new TestClass();
            //testClass.Method1();
            //testClass.Method2();
            //testClass.Method3();
            //testClass.Method4();
            //testClass.Method5();
            //testClass.Method6();

            var summary = BenchmarkRunner.Run<TestClass>();
        }

    }

    //[SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
#if DEBUG
    [CPUUsageDiagnoser]
#endif
    public class TestClass
    {
        private static string _SrcString1 = "XYZ|2024-10-22 22:11:51.6135 | INFO | Factory | UpdateInitialStatus | [1] | $$ Factory Initialized | ms |";
        //private static string _SrcString2 = "FIXIN|2024-10-22 22:11:51.6135 | INFO | Factory | UpdateInitialStatus | [1] | $$ Factory Initialized | ms |";

        private static Dictionary<int, string> _SrcString1Dict = new Dictionary<int, string>()
        {
            { 0, "AA" },
            { 1, "BB" },
            { 2, "CC" },
            { 3, "DD" },
            { 4, "EE" },
            { 5, "FF" },
            { 6, "GG" },
            { 7, "HH" }
        };

        private readonly static string msgType = "XYZ";

        private static char[] delimiters = new char[] { ',' };
        private static char delimiter = '|';

        [Benchmark]
        public void Method1()
        {
            string[] srcDatas = _SrcString1.Substring(0, _SrcString1.Length - 1).Split(delimiters);
            string[,] datas = new string[srcDatas.Length, 2];
            for (int i = 0; i < srcDatas.Length; i++)
            {
                datas[i, 0] = _SrcString1Dict[i];
                datas[i, 1] = srcDatas[i];
            }
        }

        [Benchmark]
        public void Method2()
        {
            string[] srcDatas = _SrcString1[..^1].Split(delimiters);
            string[,] datas = new string[srcDatas.Length, 2];
            for (int i = 0; i < srcDatas.Length; i++)
            {
                datas[i, 0] = _SrcString1Dict[i];
                datas[i, 1] = srcDatas[i];
            }
        }

        [Benchmark]
        public void Method3()
        {
            string[,] datas = new string[_SrcString1Dict.Count, 2];

            int start = 0;
            int index = 0;
            for (int curr = 0; curr < _SrcString1.Length; curr++)
            {
                if (_SrcString1[curr] == delimiter)
                {
                    datas[index, 0] = _SrcString1Dict[index];
                    datas[index, 1] = _SrcString1.Substring(start, curr - start);
                    start = curr + 1;
                    index++;
                }
            }
        }

        [Benchmark]
        public void Method4()
        {
            string[,] datas = new string[_SrcString1Dict.Count, 2];

            int start = 0;
            int index = 0;
            for (int curr = 0; curr < _SrcString1.Length; curr++)
            {
                if (_SrcString1[curr] == delimiter)
                {
                    datas[index, 0] = _SrcString1Dict[index];
                    datas[index, 1] = _SrcString1[start..curr];
                    start = curr + 1;
                    index++;
                }
            }
        }

        [Benchmark]
        public void Method5()
        {
            string[,] datas = new string[_SrcString1Dict.Count, 2];
            ReadOnlySpan<char> readOnlySpan = _SrcString1.AsSpan();

            int start = 0;
            int index = 0;
            for (int curr = 0; curr < _SrcString1.Length; curr++)
            {
                if (_SrcString1[curr] == delimiter)
                {
                    datas[index, 0] = _SrcString1Dict[index];
                    datas[index, 1] = readOnlySpan.Slice(start, curr - start).ToString();

                    start = curr + 1;
                    index++;
                }
            }
        }

        [Benchmark]
        public void Method6()
        {
            string[,] datas = new string[_SrcString1Dict.Count, 2];
            ReadOnlySpan<char> readOnlySpan = _SrcString1.AsSpan();

            int start = 0;
            int index = 0;
            while (true)
            {
                int pos = readOnlySpan[start..].IndexOf(delimiter);
                if (pos == -1)
                {
                    // 處理最後一個區段
                    //datas[index, 0] = _SrcString1Dict[index];
                    //datas[index, 1] = readOnlySpan.Slice(start, _SrcString1.Length).ToString();
                    break;
                }

                datas[index, 0] = _SrcString1Dict[index];
                datas[index, 1] = readOnlySpan.Slice(start, pos).ToString();

                // 更新起始位置
                start += pos + 1;
                index++;
            }
        }

        //[Benchmark]
        //public void Method7()
        //{
        //    int[,] datas = new int[_SrcString1Dict.Count, 3];
        //    ReadOnlySpan<char> readOnlySpan = _SrcString1.AsSpan();

        //    int start = 0;
        //    int index = 0;
        //    while (true)
        //    {
        //        int pos = readOnlySpan[start..].IndexOf(delimiter);
        //        if (pos == -1)
        //        {
        //            // 處理最後一個區段
        //            datas[index, 0] = index;
        //            datas[index, 1] = start;
        //            datas[index, 2] = _SrcString1.Length;
        //            break;
        //        }

        //        // 更新起始位置
        //        start += pos + 1;
        //        index++;
        //    }
        //}
    }
}