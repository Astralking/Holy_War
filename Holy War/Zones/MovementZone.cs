using System.Linq;
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
    }
}
