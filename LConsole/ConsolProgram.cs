using System;
using Lab1.Tracer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using Lab1.Tracer.serializer.ClassSerializer;

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

            Func3(4);
            Func1();
            Func1();
            var traceResult = tracer.getTraceResult();

            xmlSerializer xmlSerializ = new xmlSerializer();
            string messageXml = xmlSerializ.serialize(traceResult);
            Console.WriteLine(messageXml);
            File.WriteAllText("trace.xml", messageXml);

            Console.WriteLine("\n--------------------------------------\n");

            jsonSerializer jsonSerializ = new jsonSerializer();
            string messageJson = jsonSerializ.serialize(traceResult);
            Console.WriteLine(messageJson);
            File.WriteAllText("trace.json", messageJson);
        }

        private static void Func1()
        {
            tracer.startTrace();

            Thread.Sleep(100);
            Func2();

            tracer.stopTrace();
        }

        private static void Func2()
        {
            tracer.startTrace();

            Thread.Sleep(200);

            tracer.stopTrace();
        }

        private static void Func3(int n)
        {
            tracer.startTrace();

            Thread.Sleep(50);
            if (n == 1)
                Func1();

            if (n != 0)               
                Func3(--n);

            tracer.stopTrace();
        }
    }
}

