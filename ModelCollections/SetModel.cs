using IntersectManyCollection.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntersectManyCollection.ModelCollections
{
    public class SetModel<T,S>
    {
        int[] buckets;
        Slot[] slots;
        int count;
        int freeList;
        IEqualityComparerManyCollection<T,S> comparer;

        public SetModel(IEnumerable<S> collection, IEqualityComparerManyCollection<T, S> comparer) : this (comparer)
        {
            foreach (S item in collection) Add(item);
        }

        public SetModel(IEqualityComparerManyCollection<T,S> comparer)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            this.comparer = comparer;
            buckets = new int[7];
            slots = new Slot[7];
            freeList = -1;
        }

        public bool Add(S value)
        {
            return !Find(value, true);
        }

        public bool Contains(S value)
        {
            return Find(value, false);
        }

        public bool Remove(T value)
        {
            int hashCode = InternalGetHashCode(value);
            int bucket = hashCode % buckets.Length;
            int last = -1;
            for (int i = buckets[bucket] - 1; i >= 0; last = i, i = slots[i].next)
            {
                if (slots[i].hashCode == hashCode && comparer.Equals(value, slots[i].value))
                {
                    if (last < 0)
                    {
                        buckets[bucket] = slots[i].next + 1;
                    }
                    else
                    {
                        slots[last].next = slots[i].next;
                    }
                    slots[i].hashCode = -1;
                    slots[i].value = default(S);
                    slots[i].next = freeList;
                    freeList = i;
                    return true;
                }
            }
            return false;
        }

        bool Find(S value, bool add)
        {
            var comparerDef = EqualityComparer<S>.Default;
            int hashCode = InternalGetHashCode(value);
            for (int i = buckets[hashCode % buckets.Length] - 1; i >= 0; i = slots[i].next)
            {
                if (slots[i].hashCode == hashCode && comparerDef.Equals(slots[i].value, value)) return true;
            }
            if (add)
            {
                int index;
                if (freeList >= 0)
                {
                    index = freeList;
                    freeList = slots[index].next;
                }
                else
                {
                    if (count == slots.Length) Resize();
                    index = count;
                    count++;
                }
                int bucket = hashCode % buckets.Length;
                slots[index].hashCode = hashCode;
                slots[index].value = value;
                slots[index].next = buckets[bucket] - 1;
                buckets[bucket] = index + 1;
            }
            return false;
        }

        void Resize()
        {
            int newSize = checked(count * 2 + 1);
            int[] newBuckets = new int[newSize];
            Slot[] newSlots = new Slot[newSize];
            Array.Copy(slots, 0, newSlots, 0, count);
            for (int i = 0; i < count; i++)
            {
                int bucket = newSlots[i].hashCode % newSize;
                newSlots[i].next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }
            buckets = newBuckets;
            slots = newSlots;
        }

        internal int InternalGetHashCode(T value)
        {
            return GetHashCode(value);
        }
        internal int InternalGetHashCode(S value)
        {
            return GetHashCode(value);
        }

        internal int GetHashCode(object value) 
        {
            if (typeof(T) == typeof(S))
                return 0;
            IEqualityComparerDifferenceModels<T, S> comparerDiff = null;
            if (comparer is IEqualityComparerDifferenceModels<T, S>)
            {
                comparerDiff = (IEqualityComparerDifferenceModels<T, S>)comparer;
                if(value is T) 
                    return (value == null) ? 0 : comparerDiff.GetHashCode((T)value) & 0x7FFFFFFF;
                else
                    return (value == null) ? 0 : comparerDiff.GetHashCode((S)value) & 0x7FFFFFFF;
            }
            return 0;
        }

        internal struct Slot
        {
            internal int hashCode;
            internal S value;
            internal int next;
        }
    }
}
