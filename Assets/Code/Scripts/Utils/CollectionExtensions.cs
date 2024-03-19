using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Utils
{
    public static class CollectionExtensions
    {
        public static T GetRandom<T>(this IReadOnlyList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}