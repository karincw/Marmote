using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/CharacterData")]
    public class CharacterDataSO : ScriptableObject
    {
        public string itemName;
        public Sprite visual, attackAnime;
        public MainStat mainStat;
        public List<SynergySO> synergies = new();

        public virtual CharacterDataSO Init()
        {
            var _so = CreateInstance<CharacterDataSO>();

            _so.itemName = itemName;
            
            _so.visual = visual;
            _so.attackAnime = attackAnime;

            _so.mainStat = mainStat;
            _so.synergies = synergies.ToList();

            return _so;
        }
    }
}
