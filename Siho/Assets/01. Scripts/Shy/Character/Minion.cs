using Shy.Info;

namespace Shy.Unit
{
    public class Minion : Character
    {
        public override void Init(CharacterSO _data)
        {
            base.Init(_data);
            pressCompo.Init(InfoType.Minion, () =>
            {
                InfoManager.Instance.OpenPanel(InfoType.Minion, new CharacterInfo(this, _data));
            });
        }
    }
}
