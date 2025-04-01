using System;

namespace karin
{
    [Flags]
    public enum TileType
    {
        None = 0,
        Battle = 1,
        Elite = 2,
        Boss = 4,
        Shop = 8,
        Event = 16,
        ChangeStage = 32,
    }

}