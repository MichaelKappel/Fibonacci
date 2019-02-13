
using System;
using System.Collections.Generic;
using System.IO;
using VariableBase.Mathematics;

namespace Fibonacci_Number_Generator
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1: Fibonacci");
            Console.WriteLine("Other: EXIT");

            var env = new CharMathEnvironment("0123456789");

            var command = Console.ReadLine();
            DateTime startTime;
            if (command == "1")
            {
                UInt32 fibonaccisCount = 2758065;
                Number fibonacci1 = env.OpenNumberFile(@"D:\git\Fibonacci\Fibonacci-Number-Generator\Fibonacci\6\", @"2758064.txt");
                Number fibonacci2 = env.OpenNumberFile(@"D:\git\Fibonacci\Fibonacci-Number-Generator\Fibonacci\6\", @"2758065.txt");
                
                Boolean stop = false;
                while (!stop)
                {
                    startTime = DateTime.Now;
                    Number fibonacciNext = fibonacci1 + fibonacci2;
                    String fibonacci = fibonacciNext.GetCharArray();
                    fibonaccisCount += 1;
          
                    fibonacciNext.SaveFile(fibonacciNext.Size.ToString(), fibonaccisCount.ToString());

                    fibonacci1 = fibonacci2;
                    fibonacci2 = fibonacciNext;
                    Console.WriteLine("{0} length {1}", fibonaccisCount, fibonacciNext.Size);
                }
            }
            else
            {
                return;
            }


            Main(args);
        }
    }
}
