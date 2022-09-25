using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Distinct<T, K>(this IEnumerable<T> source, Func<T, K> predicate)
        {
            HashSet<K> sets = new HashSet<K>();
            foreach (var item in source)
            {
                if (sets.Add(predicate(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
