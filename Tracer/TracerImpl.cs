using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;


namespace Lab1.Tracer
{ // : ITracer
    public class TracerImpl 
    {
        List<ThreadInf> threadList = new List<ThreadInf>();

        public void startTrace()
        {
            //Получение id потока
            var threadId = Environment.CurrentManagedThreadId;

            var stackTrace = new StackTrace(); //Получение информации о вызывающем методе
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name; //Получить имя метода

            //Получить имя класса
            var callingMethod = stackTrace.GetFrame(1).GetMethod(); 
            var callingClassName = callingMethod.DeclaringType.Name;

            //--
            var newThread = new ThreadInf(threadId, callingMethodName, callingClassName);
            

            var targetThread = threadList.FirstOrDefault(t => t.threadId == threadId);
            if (targetThread != null)
            {
                targetThread.AddNode(newThread);
            }
            //----
            threadList.Add(newThread);
            newThread.StartTimer();
        }

        public void stopTrace() 
        {
            //Получение id потока
            var threadId = Environment.CurrentManagedThreadId;

            var stackTrace = new StackTrace(); //Получение информации о вызывающем методе
            var callingMethodName = stackTrace.GetFrame(1).GetMethod().Name; //Получить имя метода

            //Получить имя класса
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            var callingClassName = callingMethod.DeclaringType.Name;

            //----
            var targetThread = threadList.FirstOrDefault(t =>
              t.threadId == threadId &&
              t.methodName == callingMethodName &&
              t.className == callingClassName);

            if (targetThread != null)
            {
                targetThread.StopTimer();
            }
            //----
        }

        //Получить результаты измерений
        //Доработать
        public List<long> getTraceResult()
        {
            List<long> Time = new List<long>();
            foreach (ThreadInf thread in threadList)
            {
                Time.Add(thread.ResultTime());
            }
                return Time;
        }
    }
}
