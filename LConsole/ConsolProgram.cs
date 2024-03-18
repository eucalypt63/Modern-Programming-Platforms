using System;
using Lab1.Tracer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.LConsole
{
    class Program
    {
        private static readonly TracerImpl tracer = new TracerImpl();

        public static void Main()
        {
            var thread1 = new Thread(Func1);
            var thread2 = new Thread(Func2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            var traceResult = tracer.getTraceResult();
            foreach (var trace in traceResult)
            {
                Console.WriteLine(trace);
            }
           
        }

        private static void Func1()
        {
            Console.WriteLine("Func1 start");
            tracer.startTrace();
            Thread.Sleep(100);
            Func2();
            Console.WriteLine("Func1 stop");
            tracer.stopTrace();
        }

        private static void Func2()
        {
            Console.WriteLine("Func2 start");
            tracer.startTrace();

            Thread.Sleep(200);

            Console.WriteLine("Func2 stop");
            tracer.stopTrace();
        }
    }
}
