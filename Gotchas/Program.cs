using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchas
{
    class Program
    {
        private static List<Action> CreateActions(IEnumerable<string> strings)
        {
            var actions = new List<Action>();
            foreach (var s in strings)
                actions.Add(() => Console.WriteLine(s));
            return actions;
        }

        static void Main(string[] args)
        {
            object x;
            var strings = new[] { "a", "b", "c" };
            var actions = CreateActions(strings);
            actions.ForEach(f => f());
        }
    }
}
