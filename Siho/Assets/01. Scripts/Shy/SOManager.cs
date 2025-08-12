using System.Collections.Generic;
using UnityEngine;
using Shy.Event;

namespace Shy
{
    public class SOManager : MonoBehaviour
    {
        public static SOManager Instance;

        [SerializeField] private List<ScriptableObject> so;
        
        private Dictionary<SynergyType, SynergySO> synergyDic = new();
        private Dictionary<BattleEvent, BattleEventSO> battleEventDic = new();

        private void Awake()
        {
            if(Instance != null) { Destroy(gameObject); return; }
            Instance = this;
        }

        private void Start()
        {
            foreach (var _item in so)
            {
                if(_item is SynergySO _synergyItem)
                {
                    synergyDic.Add(_synergyItem.synergyType, _synergyItem);
                }
                else if(_item is BattleEventSO _battleEventItem)
                {
                    battleEventDic.Add(_battleEventItem.eventType, _battleEventItem);
                }
            }
        }

        public SynergySO GetSO(SynergyType _type) => synergyDic[_type];
        public BattleEventSO GetSO(BattleEvent _type) => battleEventDic[_type];
    }
}