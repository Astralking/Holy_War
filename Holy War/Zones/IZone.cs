using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Zones
{
    public interface IZone
    {
        HighlightTile[,] Grid { get; set; }
        void ResetOrigin(Point newOrigin);
        void CalculateGrid(int x, int y);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
        bool PositionIsInZone(Point position);
    }
}
