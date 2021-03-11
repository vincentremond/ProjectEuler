using System;
using System.Linq;

namespace TestCSharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            P003b.EntryPoint().ToList().ForEach(tuple => Console.WriteLine(tuple));
        }
    }
}
