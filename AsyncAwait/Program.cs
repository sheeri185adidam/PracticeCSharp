using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = System.Console;

namespace AsyncAwait
{
    class Program
    {
        private static string _value = null;

        private static async Task<string> WorkAsync(int number)
        {
            var task = Task.Run(() =>
            {
                for (var i = 0; i < 10; ++i)
                {
                    number++;
                    Task.Delay(100);
                }
                return number.ToString();
            });

            await task;
            return "Result from task: " + task.Result;
        }

        private static void WorkAsync2(int number)
        {
            var task = Task.Run(() =>
            {
                for (var i = 0; i < 10; ++i)
                {
                    number++;
                    Task.Delay(100);
                }

                return number.ToString();
            });
            task.ConfigureAwait(false)
                .GetAwaiter()
                .OnCompleted(() => { _value = "Result from task: " + task.Result;});
        }

        static void Main(string[] args)
        {
            WorkAsync2(100);
            Console.WriteLine($"Value of work(before loop): {_value}");
            for (var i = 0; i < 10; ++i)
            {
                Console.WriteLine($"Value of i: {i}");
                Thread.Sleep(200);
            }
            Console.WriteLine($"Value of work(after loop): {_value}");

            Console.ReadKey();
        }
    }
}
