using Shy.Info;

namespace Shy.Unit
{
    public class Minion : Character
    {
        public override void Init(Team _team, CharacterSO _data)
        {
            base.Init(_team, _data);
            pressCompo.Init(InfoType.Minion, () =>
            {
                InfoManager.Instance.OpenPanel(InfoType.Minion, new CharacterInfo(this, _data));
            });
        }
    }
}
