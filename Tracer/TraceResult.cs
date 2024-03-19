using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer
{
    public class TraceResult
    {
        List<ThreadInf> TNodeHead = new List<ThreadInf>();
        public void getTraceResult(List<ThreadInf> nodeList)
        {
            foreach (ThreadInf node in nodeList) 
            {
                ThreadInf Head = node.GetHead();
                if (!TNodeHead.Contains(Head))
                {
                    TNodeHead.Add(Head);
                }
            }
        }

        // Содержит функции серелизации json

        // Содержит функции серелизации xml
    }
}
