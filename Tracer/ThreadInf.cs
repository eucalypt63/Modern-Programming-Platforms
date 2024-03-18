using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Lab1.Tracer
{
    public class ThreadInf
    {
        ThreadInf TParent = null;
        List<ThreadInf> TChilds = new List<ThreadInf>();
        public int threadId { get; }
        public string methodName { get; }
        public string className { get; }
        public Stopwatch stopwatch = new Stopwatch();

        public bool isActive;

        public ThreadInf(int id, string MethodName, string ClassName)
        {
            threadId = id;
            methodName = MethodName;
            className = ClassName;
        }

        //Запустить таймер
        public void StartTimer()
        {
            isActive = true;
            stopwatch.Start();
        }

        //Остановить таймер
        public void StopTimer()
        {
            isActive = false;
            stopwatch.Stop();
        }

        //Получить результат таймера
        public long ResultTime()
        {
            return stopwatch.ElapsedMilliseconds;
        }

        public void AddNode(ThreadInf node)
        {
            ThreadInf curNode = this;
            bool flag = false;
            while (!flag)
            {
                flag = true;
                foreach (ThreadInf child in curNode.TChilds)
                {
                    if (child.isActive)
                    {
                        flag = false;
                        curNode = child;
                        break;
                    }
                }
            }
            curNode.TChilds.Add(node);
        }

        //Получить корневой узел
        public ThreadInf GetHead()
        {
            ThreadInf Head = this;
            while (Head.TParent != null) 
            {
                Head = Head.TParent;
            }

            return Head;
        }

    }
}
