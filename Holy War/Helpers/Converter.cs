using System;
using Holy_War.Enumerations;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;

namespace Holy_War.Helpers
{
    public static class Converter
    {
        public static Point OrientationToDirection(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return new Point(0, -1);
                    break;
                case Orientation.South:
                    return new Point(0, 1);
                    break;
                case Orientation.East:
                    return new Point(1, 0);
                    break;
                case Orientation.West:
                    return new Point(-1, 0);
                    break;
            }
    
            return new Point(0,0);
        }

        public static int PointToGridPoint(float coord)
        {
           return (int)Math.Round(coord / (float) Tile.GetDefaultTileWidth()) * Tile.GetDefaultTileWidth();
        }

        public static int TileWidthToPixels(int tileNumber)
        {
            return tileNumber * Tile.GetDefaultTileWidth();
        }

        public static int TileHeightToPixels(int tileNumber)
        {
            return tileNumber * Tile.GetDefaultTileHeight();
        }

        public static int PixelWidthToTile(int coord)
        {
            return coord * Tile.GetDefaultTileWidth();
        }

        public static int PixelHeightToTile(int coord)
        {
            return coord * Tile.GetDefaultTileHeight();
        }
    }
}
