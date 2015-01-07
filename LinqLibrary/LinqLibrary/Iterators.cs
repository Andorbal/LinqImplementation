using System;
using System.Collections.Generic;

namespace LinqLibrary
{
    public class Iterators
    {
        public IEnumerator<string> Iterate()
        {
            yield return "Foo";
            yield return "Bar";

            if (DateTime.Now.Millisecond % 13 == 0)
            {
                yield break;    
            }

            yield return "Baz";
        }
    }
}
