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
                case Orientation.South:
                    return new Point(0, 1);
                case Orientation.East:
                    return new Point(1, 0);
                case Orientation.West:
                    return new Point(-1, 0);
            }
    
            return new Point(0,0);
        }

        public static Vector2 GetLocationInPixels(Vector2 location)
        {
            return new Vector2(location.X * Tile.GetDefaultTileWidth(), location.Y * Tile.GetDefaultTileHeight());
        }

        public static Vector2 GetLocationInPixels(Point location)
        {
            return new Vector2(location.X * Tile.GetDefaultTileWidth(), location.Y * Tile.GetDefaultTileHeight());
        }

        public static Vector2 GetLocationInTiles(Point location)
        {
            return new Vector2(location.X, location.Y);
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

        public static Vector2 PointToVector(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point VectorToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static int PixelHeightToTile(int coord)
        {
            return coord * Tile.GetDefaultTileHeight();
        }

        public static float LayerTofloat(Layer layer)
        {
            return (float)layer * 0.1f;
        }
    }
}
