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
        private readonly int _distance;
        private readonly int _zoneSize;

        public int Distance { get { return _distance; } }
        public HighlightTile[,] Grid { get; set; }

        public Zone(int distance, Point origin)
        {
            _distance = distance;
            _zoneSize = GetZoneSize(distance);

            CalculateGrid(origin.X, origin.Y);
        }

        public void ResetOrigin(Point newOrigin)
        {
            CalculateGrid(newOrigin.X, newOrigin.Y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        private void CalculateGrid(int x, int y)
        {
            Grid = new HighlightTile[MainGame.CurrentWorld.WidthInTiles, MainGame.CurrentWorld.HeightInTiles];

            for (var i = x - _distance; i <= x + _distance; ++i)
            {
                for (var j = y - _distance; j <= y + _distance; ++j)
                {
                    if (i < 0 || j < 0 || i >= MainGame.CurrentWorld.WidthInTiles || j >= MainGame.CurrentWorld.HeightInTiles) 
                        continue;

                    if (Math.Abs(x - i) + Math.Abs(y - j) > _distance) 
                        continue;

                    if (i == x && j == y) 
                        continue;

                    Grid[i, j] = new HighlightTile(
                        TextureManager.Texture["Boxes/BlueHighlightBox"],
                        new Point(i, j), 
                        Layer.Master);
                }
            }
        }

        private static int GetZoneSize(int distance)
        {
            return (distance * 2) + 1;
        }
    }
}

