using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/CharacterData")]
    public class CharacterDataSO : ScriptableObject
    {
        public string itemName;
        public Sprite visual, attackAnime;
        public MainStat mainStat;

        public List<SynergySO> baseSynergies;
        internal Dictionary<SynergyType, int> synergies = new();


        public virtual CharacterDataSO Init()
        {
            var _so = CreateInstance<CharacterDataSO>();

            _so.itemName = itemName;
            
            _so.visual = visual;
            _so.attackAnime = attackAnime;

            _so.mainStat = mainStat;

            foreach (var item in baseSynergies)
            {
                _so.AddSynergy(item.synergyType);
            }

            return _so;
        }

        public void AddSynergy(SynergyType _type)
        {
            if (synergies.ContainsKey(_type))
            {
                if (SOManager.Instance.GetSOLimitValue(_type) > synergies[_type])
                    synergies[_type]++;
            }
            else
            {
                synergies.Add(_type, 1);
            }
        }
    }
}
