using DG.Tweening;
using UnityEngine;

public class Utils
{
    public static void FadeGroup(CanvasGroup group, float time, bool open, Ease ease = Ease.Linear)
    {
        group.DOFade(open ? 1 : 0, time).SetEase(ease)
            .OnComplete(() =>
            {
                group.interactable = open;
                group.blocksRaycasts = open;
            });
    }
}