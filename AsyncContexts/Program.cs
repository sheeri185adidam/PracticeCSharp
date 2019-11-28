using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncContexts
{
    class Program
    {
        static Task<int> MethodB()
        {
            //Do some work here - WorkB
            return Task.FromResult((int)100);
        }

        static async Task<int> MethodA()
        {
            //Do some work here - WorkA1
            var task = MethodB();
            await task;
            //Do some work here - WorkA2
            return task.Result;
        }

        private static void Main(string[] args)
        {
            var context = ExecutionContext.Capture();
            //Do some work here - WorkMain1
            var taskA = MethodA();
            //Do some work here - WorkMain2
            var result = taskA.Result;
        }
    }
}
