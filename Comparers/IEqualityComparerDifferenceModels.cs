using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectManyCollection.Comparers
{
    public interface IEqualityComparerDifferenceModels<T,S> : IEqualityComparerManyCollection<T, S> 
    {
        int GetHashCode(S obj);
    }
}
