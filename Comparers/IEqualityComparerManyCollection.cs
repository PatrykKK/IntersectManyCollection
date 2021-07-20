using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectManyCollection.Comparers
{
    public interface IEqualityComparerManyCollection<in T, in S>
    {
        bool Equals(T x, S y);
        int GetHashCode(T obj);
    }
}
