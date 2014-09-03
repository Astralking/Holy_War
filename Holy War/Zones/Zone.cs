using System;
using System.IO;
using Holy_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;
using Holy_War.Tiles;

namespace Holy_War.Zones
{
    public class Zone : IZone
    {
        internal readonly int _distance;
        internal readonly int _zoneSize;
        internal readonly Point _origin;

        public int Distance { get { return _distance; } }
        public HighlightTile[,] Grid { get; set; }

        public Zone(int distance, Point origin)
        {
            _distance = distance;
            _origin = origin;
            _zoneSize = GetZoneSize(distance);
           
            ResetGrid();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void ResetOrigin(Point newOrigin)
        {
        }

        public virtual void CalculateGrid(int x, int y)
        {
        }

        internal bool PositionIsInZone(Point position)
        {
            if (_distance == 0)
                return true;

            if (position.X < 0
                || position.Y < 0
                || position.X >= MainGame.CurrentWorld.WidthInTiles
                || position.Y >= MainGame.CurrentWorld.HeightInTiles)
                return false;

            return Grid[position.X, position.Y] != null;
        }

        private static int GetZoneSize(int distance)
        {
            return (distance * 2) + 1;
        }

        internal void ResetGrid()
        {
            Grid = new HighlightTile[MainGame.CurrentWorld.WidthInTiles, MainGame.CurrentWorld.HeightInTiles];
        }


    }
}

