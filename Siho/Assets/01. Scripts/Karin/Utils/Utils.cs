using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="fade"> true => 0 | false => 1</param>
        /// <param name="time"></param>
        public static void FadeCanvasGroup(CanvasGroup group, bool fade, float time, Action callback = null)
        {
            if (group == null) return;
            if (time == 0)
            {
                group.alpha = fade ? 0 : 1;
                group.interactable = fade ? false : true;
                group.blocksRaycasts = fade ? false : true;
                callback?.Invoke();
            }
            else
            {
                group.DOFade(fade ? 0 : 1, time).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        group.interactable = fade ? false : true;
                        group.blocksRaycasts = fade ? false : true;
                        callback?.Invoke();
                    });
            }
        }
    }
}
