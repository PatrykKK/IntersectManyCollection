using IntersectManyCollection.Comparers;
using IntersectManyCollection.ModelCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectManyCollection.Extensions
{
    public static class LinqExtension
    {
        public static IEnumerable<T> IntersectManyCollections<T,S>(this IEnumerable<T> source, IEnumerable<IEnumerable<S>> manyList, IEqualityComparerManyCollection<T,S> equalityComparerManyCollection)
        {
            bool isInAllCollections;
            HashSet<SetModel<T,S>> manyCollections = new HashSet<SetModel<T,S>>(10000);
            foreach (IEnumerable<S> item in manyList) manyCollections.Add(new SetModel<T, S>(item, equalityComparerManyCollection));
            foreach (T item in source)
            {
                isInAllCollections = true;
                foreach (var colletion in manyCollections)
                    if (!colletion.Remove(item))
                        isInAllCollections = false;
                if (isInAllCollections)
                    yield return item;
            }        
        }
    }
}
