
using karin.worldmap;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin
{
    class Utils
    {
        public static List<T> ShuffleList<T>(List<T> list)
        {
            return list.OrderBy(e => Random.value).ToList();
        }
    }
}