using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntersectManyCollection.Comparers
{
    public class ModelComparer<T, S> : IEqualityComparerDifferenceModels<T, S>
    {
        private readonly Func<T, S, bool> _comparerFunc;
        public ModelComparer(Func<T,S,bool> func)
        {
            _comparerFunc = func;
        }
        public bool Equals(T x, S y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (_comparerFunc.Invoke(x, y))
                return true;
            else
                return false;
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }

        public int GetHashCode(S obj)
        {
            return 0;
        }
    }
}
