using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Tiles;
using Holy_War.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors
{
    public class UserActor : Actor
    {
        //private readonly HighlightTile _highlightTile;
        private readonly MovementZone _movementZone;

        public UserActor(Texture2D texture, Point location, Layer layer, ActorStats stats)
            : base(texture, location, layer, stats)
        {
            //_highlightTile = new HighlightTile(TextureManager.Texture["Boxes/RedHighlightBox"], location, Layer.Master);

            _movementZone = 
                stats != null 
                ? new MovementZone(stats.Movement, location) 
                : new MovementZone(0, location);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (MainGame.CurrentWorld.SelectedUserActor == this)
                HighlightZones(spriteBatch);             

            base.Draw(spriteBatch);
        }

        public override void Move(Point direction, GameTime gameTime)
        {
            ScreenLocation += direction;
                //.X * (int)gameTime.ElapsedGameTime.TotalSeconds, 
                //direction.Y * (int)gameTime.ElapsedGameTime.TotalSeconds);

            //_highlightTile.ScreenLocation += direction;

            base.Move(direction, gameTime);
        }

        public virtual void Action(UserActor userActor)
        {
            Updated = true;

            _movementZone.ResetOrigin(ScreenLocation);

            MainGame.CurrentWorld.SelectSelectionBox();

            base.Action();
        }

        public void Back()
        {
            if (this is SelectionBoxActor)
                return;

            ScreenLocation = StartingPosition;
            
            MainGame.CurrentWorld.SelectSelectionBox();
        }

        public void HighlightZones(SpriteBatch spriteBatch)
        {
            //_highlightTile.Draw(spriteBatch);           
            _movementZone.Draw(spriteBatch);
        }
    }
}
