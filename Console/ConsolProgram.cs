using System;
using Lab1.Tracer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.Console
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

        }

        private static void Func1()
        {

            Thread.Sleep(100);
            Func2();

        }

        private static void Func2()
        {
            Thread.Sleep(200);
        }
    }
}
