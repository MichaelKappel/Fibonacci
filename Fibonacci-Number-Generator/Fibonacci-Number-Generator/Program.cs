
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
            Console.WriteLine("2: Fibonacci primes");
            Console.WriteLine("Other: EXIT");

            var env = new CharMathEnvironment("0123456789");

            var command = Console.ReadLine();
            DateTime startTime;
            if (command == "1")
            {
                //UInt32 fibonaccisCount = 0;
                //IList<String> fibonaccis = new List<String>();
                //using (FileStream fs = File.Open("../../../../Fibonacci.txt", FileMode.OpenOrCreate))
                //{
                //    using (StreamReader sr = new StreamReader(fs))
                //    {
                //        while (sr.Peek() >= 0)
                //        {
                //            fibonaccis.Add(sr.ReadLine());
                //            fibonaccisCount += 1;
                //        }
                //    }
                //}


                UInt32 fibonaccisCount = 3;
                IList<String> fibonaccis = new List<String>();
                //var env2 = new CharMathEnvironment();

                if (fibonaccis.Count == 0)
                {
                    fibonaccis.Add("1");
                    fibonaccis.Add("2");
                }

                Number fibonacci1 = env.GetNumber(fibonaccis[fibonaccis.Count - 2]);
                Number fibonacci2 = env.GetNumber(fibonaccis[fibonaccis.Count - 1]);

                fibonaccis.Clear();


                Boolean stop = false;
                while (!stop)
                {
                    startTime = DateTime.Now;
                    Number fibonacciNext = fibonacci1 + fibonacci2;
                    String fibonacci = fibonacciNext.GetActualValue();
                    fibonaccisCount += 1;
                    using (FileStream fs = File.Open(String.Format("../../../../Fibonacci/{0}.txt", fibonaccisCount.ToString().PadLeft(100, '0')), FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(fibonacci);
                            sw.Flush();
                        }
                    }

                    //TimeSpan span = DateTime.Now - startTime;
                    //Console.WriteLine(fibonacci);
                    //Console.WriteLine("Time:{0}", span);

                    fibonacci1 = fibonacci2;
                    fibonacci2 = fibonacciNext;

                }
            }
            else if (command == "2")
            {
                var env2 = new CharMathEnvironment();

                IList<String> fibonaccis = new List<String>();
                using (FileStream fs = File.Open("../../../../FibonacciPrime.txt", FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (sr.Peek() >= 0)
                        {
                            fibonaccis.Add(sr.ReadLine());
                        }
                    }
                }

                using (FileStream fs = File.Open("../../../../FibonacciPrime.txt", FileMode.Append))
                {
                    Number fibonacci2;
                    Number fibonacci1;
                    if (fibonaccis.Count == 0)
                    {
                        fibonacci1 = env2.KeyNumber[1];
                        fibonacci2 = env2.SecondNumber;
                    }
                    else
                    {
                        String fibonaccisRaw = fibonaccis[fibonaccis.Count - 1];
                        String[] fibonaccisRawArray = fibonaccisRaw.Split(' ');

                        Number fibonacci0 = env.GetNumber(fibonaccisRawArray[0]).Convert(env2);
                        fibonacci1 = env.GetNumber(fibonaccisRawArray[0]).Convert(env2);
                        fibonacci2 = env.GetNumber(fibonaccisRawArray[1]).Convert(env2);

#if DEBUG
                        if (fibonacci0 + fibonacci1 != fibonacci2)
                        {
                            throw new Exception("fibonacci addition Error");
                        }
                        else if (fibonacci2 - fibonacci1 != fibonacci0)
                        {
                            throw new Exception("fibonacci addition Error");
                        }
#endif
                    }

                    fibonaccis.Clear();

                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.AutoFlush = true;
                        Boolean stop = false;
                        while (!stop)
                        {
                            startTime = DateTime.Now;
                            Number fibonacciNext = fibonacci1 + fibonacci2;
#if DEBUG
                            if (fibonacciNext - fibonacci1 != fibonacci2)
                            {
                                throw new Exception("fibonacci addition Error");
                            }
#endif
                            if (fibonacciNext.IsPrime())
                            {
                                String fibonacci = String.Format("{0} {1} {2}", fibonacci1.ToString(env), fibonacci2.ToString(env), fibonacciNext.ToString(env));
                                sw.WriteLine(fibonacci);
                                Console.WriteLine(fibonacci);

                                TimeSpan span = DateTime.Now - startTime;
                                Console.WriteLine("Time:{0}", span);
                            }
                            fibonacci1 = fibonacci2;
                            fibonacci2 = fibonacciNext;


                        }
                    }
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
