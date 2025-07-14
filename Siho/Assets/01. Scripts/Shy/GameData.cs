namespace Shy.Data
{
    public static class GameData
    {
        public static CharacterDataSO playerData;

        public static void Init(CharacterDataSO _player)
        {
            playerData = _player.Init();
        }
    }
}