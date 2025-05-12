using UnityEngine;
using System.Collections.Generic;

namespace Shy
{
    public class BuffManager : MonoBehaviour
    {
        public static BuffManager Instance;

        public List<BuffItem> buffs;
        private Dictionary<BuffType, BuffItem> buffDictionary = new Dictionary<BuffType, BuffItem>();

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            for (int i = 0; i < buffs.Count; i++) buffDictionary.Add(buffs[i].buff, buffs[i]);
        }

        public bool IsCountDownBuff(BuffType _buffType) => buffDictionary[_buffType].canCountDown;
        public Sprite GetSprite(BuffType _buffType) => buffDictionary[_buffType].sprite;
    }
}
