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
            Func3(0);
            var traceResult = tracer.getTraceResult();


            //--
            var traceResultT = tracer.getTraceResultLing(); //!!
            foreach (var trace in traceResultT)
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

        //Доработать обработку рекурсивного вызвова
        private static void Func3(int n)
        {
            Console.WriteLine("Func3 start");
            tracer.startTrace();

            Thread.Sleep(50);
            if (n != 4)               
                Func3(++n);
            
            Console.WriteLine("Func3 stop");
            tracer.stopTrace();
        }
    }
}
