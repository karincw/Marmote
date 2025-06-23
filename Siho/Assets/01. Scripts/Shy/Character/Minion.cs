using Shy.Info;

namespace Shy.Unit
{
    public class Minion : Character
    {
        public override bool Init(CharacterSO _data)
        {
            if (!base.Init(_data)) return false;
            pressCompo.Init(InfoType.Minion, () =>
            {
                InfoManager.Instance.OpenPanel(InfoType.Minion, new CharacterInfo(this, _data));
            });
            return true;
        }
    }
}
