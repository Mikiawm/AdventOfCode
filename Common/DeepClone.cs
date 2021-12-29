using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class DeepClone
    {
        public static List<List<T>> Clone<T>(this List<List<T>> twoDList)
        {
            if (twoDList != null)
            {
                List<List<T>> result = new List<List<T>>();
                for (int i = 0; i < twoDList.Count(); i++)
                {
                    List<T> aList = new List<T>();
                    for (int j = 0; j < twoDList.ElementAt(i).Count(); j++)
                    {
                        aList.Add(twoDList.ElementAt(i).ElementAt(j));
                    }

                    result.Add(aList);
                }

                return result;
            }

            return null;
        }

        public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>
            (Dictionary<TKey, TValue> original)
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue) entry.Value);
            }

            return ret;
        }
    }
}