using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors
{
    public class UserActor : Actor
    {
        private readonly HighlightTile _highlightTile;

        public UserActor(Texture2D texture, Point location, Layer layer, Texture2D highlightTexture, ActorStats stats)
            : base(texture, location, layer, stats)
        {
            _highlightTile = new HighlightTile(highlightTexture, location, Layer.Master);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(MainGame.SelectedUserActor == this)
                _highlightTile.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public override void Move(Point direction)
        {
            var newLocation = new Point(direction.X * Tile.GetDefaultTileWidth(), direction.Y * Tile.GetDefaultTileHeight());

            Location += newLocation;
            _highlightTile.Location += newLocation;

            base.Move(direction);
        }

        public virtual void Action(UserActor userActor)
        {
            base.Action();
        }

        public void Back()
        {
            MainGame.SelectSelectionBox();
        }
    }
}
