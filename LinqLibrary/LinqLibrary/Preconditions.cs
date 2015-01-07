using System;

namespace LinqLibrary
{
    public class Preconditions
    {
        public static T CheckNotNull<T>(T source, string paramName) where T : class 
        {
            if (source == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return source;
        }
    }
}