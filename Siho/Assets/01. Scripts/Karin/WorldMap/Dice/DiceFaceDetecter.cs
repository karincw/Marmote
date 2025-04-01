using System;
using UnityEngine;

namespace karin.worldmap.dice
{
    public class DiceFaceDetecter : MonoBehaviour
    {
        [Tooltip("바닥에 해당면이 닿았을때 출력될 넘버\n쉽게말해 반대편의 숫자")]
        public int CurrntNumber;
        [HideInInspector] public Dice dice;

        public void Init(Dice owner)
        {
            dice = owner;
        }
    }
}