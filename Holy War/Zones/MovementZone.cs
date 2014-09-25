using System;
using System.Linq;
using Holy_War.Algorithms;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Zones
{
    public class MovementZone : Zone, IZone
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
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < Grid.GetLength(0); i++)
            {
                for (var j = 0; j < Grid.GetLength(1); j++)
                {                 
                    if (Grid[i, j] != null && Grid[i, j].Animating)
                        Grid[i, j].Update(gameTime);
                }
            }
        }

        public override void ResetOrigin(Point newOrigin)
        {
            CalculateGrid(newOrigin.X, newOrigin.Y);
        }

        public override void CalculateGrid(int originX, int originY)
        {
            ResetGrid();

            var graph = DijkstrasAlgorithm.GenerateZone(new Point(originX, originY), Distance);
            
            for (var i = originX - _distance; i < originX + _distance; ++i)
            {
                for (var j = originY - _distance; j < originY + _distance; ++j)
                {
                    if (i < 0 || j < 0 || i >= GameScreen.CurrentWorld.WidthInTiles || j >= GameScreen.CurrentWorld.HeightInTiles)
                        continue;

                    if (Math.Abs(originX - i) + Math.Abs(originY - j) > _distance)
                        continue;

                    if (i == originX && j == originY)
                        continue;

                    if(graph[i, j].Distance <= Distance)
                        Grid[i, j] = new HighlightTile(
                            SpriteManager.Textures["Boxes/BlueHighlightBox"],
                            new Point(i, j),
                            Layer.Zones);
                }
            }
        }

        private void Initialise(int originX, int originY)
        {
            CalculateGrid(originX, originY);
        }
    }
}
