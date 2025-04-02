using System;

namespace karin
{
    [Flags]
    public enum TileType
    {
        None = 0,
        Battle = 1,
        Boss = 2,
        Shop = 4,
        Event = 8,
        ChangeStage = 16,
    }

}