using Shy.Info;
using UnityEngine;

namespace Shy.Unit
{
    public class Minion : Character
    {
        public override bool Init(CharacterSO _data)
        {
            Debug.Log("fd");
            if (base.Init(_data) == false) return false;
                Debug.Log("Init");

            pressCompo.Init(InfoType.Minion, () =>
            {
                InfoManager.Instance.OpenPanel(InfoType.Minion, new Info.CharacterInfo(this, _data));
            });
            return true;
        }
    }
}
