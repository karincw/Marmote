namespace Shy
{
    [System.Serializable]
    public struct Stat
    {
        public int hp;
        public int str;
        public int def;
    }

    [System.Serializable]
    public struct GetStat
    {
        public string key;
        public StatEnum stat;
        public ActionWay target;
    }

}