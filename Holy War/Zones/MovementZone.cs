using System;
using System.Linq;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Zones
{
    public class MovementZone : Zone
    {
        public MovementZone(int size, Point origin)
            : base(size, origin)
        {
            Initialise(origin.X, origin.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var highlightTile in Grid
                .Cast<HighlightTile>()
                .ToList()
                .Where(highlightTile => highlightTile != null))
                highlightTile.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void ResetOrigin(Point newOrigin)
        {
            CalculateGrid(newOrigin.X, newOrigin.Y);
        }

        public override void CalculateGrid(int x, int y)
        {
            ResetGrid();

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

        private void Initialise(int x, int y)
        {
            CalculateGrid(x, y);
        }
    }
}
