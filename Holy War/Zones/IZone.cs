using Holy_War.Tiles;
using Microsoft.Xna.Framework;

namespace Holy_War.Zones
{
    public interface IZone
    {
        HighlightTile[,] Grid { get; set; }
        void ResetOrigin(Point newOrigin);
        void CalculateGrid(int x, int y);
    }
}
