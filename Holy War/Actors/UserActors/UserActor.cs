using System.Xml.Schema;
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
        private readonly MovementZone _movementZone;
        public ActorStats Stats { get; private set; }

        public UserActor(Texture2D texture, Point location, Layer layer, ActorStats stats)
            : base(texture, location, layer)
        {
            Stats = stats;

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

        public void ResetZoneOrigins(Point newZoneOrigin)
        {
            _movementZone.ResetOrigin(newZoneOrigin);
        }

        public override void Move(Point direction, GameTime gameTime)
        {
            if (ScreenLocation + direction == StartingPosition || 
                _movementZone.PositionIsInZone(ScreenLocation + direction))
                ScreenLocation += direction;

            base.Move(direction, gameTime);
        }

        public virtual void Action(UserActor userActor)
        {
            if (ScreenLocation == StartingPosition)
            {
                MainGame.CurrentWorld.SelectSelectionBox();
                return;
            }

            if (IsValidPosition())
            {
                Updated = true;

                ResetZoneOrigins(ScreenLocation);

                MainGame.CurrentWorld.SelectSelectionBox();

                base.Action();
            }
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

        private bool IsValidPosition()
        {
            return ScreenLocation != StartingPosition &&
                   MainGame.CurrentWorld.GroundMapArray[ScreenLocation.X, ScreenLocation.Y] == null;
        }
    }
}
