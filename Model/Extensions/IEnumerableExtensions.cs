using System;
using System.Collections.Generic;
using System.Linq;
namespace Model.Extensions {
    public static class EnumerableExtensions {
        public static IEnumerable<IEnumerable<T>> GroupWhile<T>(this IEnumerable<T> seq, Func<T,T,bool> condition)
        {
            var enumerable = seq as T[] ?? seq.ToArray();
            var prev = enumerable.First();
            var list = new List<T>() { prev };

            foreach(var item in enumerable.Skip(1))
            {
                if(condition(prev,item)==false)
                {
                    yield return list;
                    list = new List<T>();
                }
                list.Add(item);
                prev = item;
            }

            yield return list;
        }
    }
}
