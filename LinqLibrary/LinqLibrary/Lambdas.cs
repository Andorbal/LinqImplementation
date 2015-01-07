using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLibrary
{
    public class Lambdas
    {
        public Action simple = () => Console.WriteLine("This is a simple lambda");

        public Func<string> Capture(string stuff)
        {
            var upper = stuff.ToUpper();

            return () => upper;
        }

        public Func<string, Func<string>> CaptureTwo(string stuff)
        {
            var upper = stuff.ToUpper();

            return (x) => () => x + upper;
        }

        public void Foo(string text)
        {
            var actions = new List<Action>();

            for (int i = 0; i < text.Length; i++)
            {
                int copy = i;
                actions.Add(() => Console.WriteLine(text[copy]));
            }

            foreach (var action in actions)
            {
                action();
            }
        }

        //public Func<List<int>> CaptureDeep(int count)
        //{
        //    List<int> items = new List<int>();
        //} 
    }
}
