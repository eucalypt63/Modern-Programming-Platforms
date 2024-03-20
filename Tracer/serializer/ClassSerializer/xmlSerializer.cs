﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Tracer.serializer.ClassSerializer
{
    public class xmlSerializer : ITraceSerializer
    {

        public string serialize(TraceResult traceResult)
        {
            string result = "";
            result += "<root>\n";

            foreach (TThread thred in traceResult.trace)
            {
                result += addThreads(thred);
            }

            result += "</root>";
            return result;
        }

        public string addThreads(TThread trace)
        {
            string result = "";
            result = result.PadRight(result.Length + 4);
            result += $"<thread id=\"{trace.id}\" time=\"{trace.time}ms\">\n";
            foreach (ThreadInf method in trace.TNodeHead)
                result += addMethod(method, 8);

            result = result.PadRight(result.Length + 4);
            result += "</thread>\n";

            return result;
        }

        public string addMethod(ThreadInf method, int step)
        {
            string result = "";
            result = result.PadRight(result.Length + step);
            if (method.TChilds.Count != 0)
            {
                result += $"<method name=\"{method.methodName}\" time=\"{method.ResultTime()}ms\" class=\"{method.className}\">\n";

                foreach (ThreadInf child in method.TChilds)
                {
                    result += addMethod(child, step + 4);
                }

                result = result.PadRight(result.Length + step);
                result += "</method>\n";
            }
            else
            {
                result += $"<method name=\"{method.methodName}\" time=\"{method.ResultTime()}ms\" class=\"{method.className}\"/>\n";
            }
            return result;
        }
    }
}