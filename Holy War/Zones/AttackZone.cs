using System;
using System.Collections.Generic;
using System.Linq;
using Holy_War.Actors.UserActors.BoxActors;
using Holy_War.Algorithms;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Zones
{
    public class AttackZone : Zone, IZone
    {
        public AttackZone(int distance, Point origin) 
            : base(distance, origin)
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

            base.ResetOrigin(newOrigin);
        }

        public override void CalculateGrid(int originX, int originY)
        {
            ResetGrid();

            var intGrid = ManhattenDistanceAlgorithm.GenerateGrid(originX, originY, Distance);

            for (int i = 0; i < intGrid.GetLength(0); i++)
            {
                for (int j = 0; j < intGrid.GetLength(1); j++)
                {
                    if(intGrid[i,j] == 1)
                        Grid[i, j] = new HighlightTile(
                            SpriteManager.Textures["Boxes/RedHighlightBox"],
                            new Point(i, j),
                            Layer.Zones);
                }
            }
        }

        private void Initialise(int x, int y)
        {
            CalculateGrid(x, y);
        }
    }
}
