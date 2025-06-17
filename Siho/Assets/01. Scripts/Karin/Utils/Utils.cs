using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin
{
    public static class Utils
    {
        public static List<T> ShuffleList<T>(List<T> list)
        {
            var copiedList = list.ToList(); // 원본 복사

            for (int i = copiedList.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (copiedList[i], copiedList[j]) = (copiedList[j], copiedList[i]);
            }

            return copiedList;
        }
    }
}
