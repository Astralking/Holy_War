using Holy_War.Tiles;

namespace Holy_War.Zones
{
    public interface IZone
    {
        HighlightTile[,] Grid { get; set; }
    }
}
