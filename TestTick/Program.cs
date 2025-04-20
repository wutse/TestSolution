// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using TestTick;

Console.WriteLine("Hello, World!");

Dictionary<ITest, int> dics = new Dictionary<ITest, int>();
Test1 t1 = new Test1();
Test2 t2 = new Test2();

dics.Add(t1, 1);
dics.Add(t2, 2);
dics.Add(new Test1(), 3);
dics.Add(new Test2(), 4);

var tt = System.Collections.Comparer.Default;
int result = tt.Compare(t1, t2);

var t = Comparer<ITest>.Default;
Console.WriteLine(t.ToString());

TestTick.Class1 class1 = new TestTick.Class1();
class1.Test();

Thread.Sleep(5000);


