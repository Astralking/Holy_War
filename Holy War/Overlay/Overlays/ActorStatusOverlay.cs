using Holy_War.Actors.UserActors;
using Holy_War.Enumerations;
using Holy_War.Events;
using Holy_War.Helpers;
using Holy_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Overlay.Overlays
{
    public class ActorStatusOverlay : Overlay
    {
		private bool _drawn;
		UserActorWithStats currentSelectionUserActor;
		UserActorWithStats currentTargetUserActor;
		
        public override void Draw(SpriteBatch spriteBatch)
        {
			if (currentSelectionUserActor != null)
			{
				spriteBatch.DrawString(SpriteManager.Fonts["Fonts/ContextMenuFont"],
					 currentSelectionUserActor.Stats.HP.ToString(),
					 new Vector2(ScreenCentre.X / 2, ScreenCentre.Y + (ScreenCentre.Y / 2)),
					 Color.Black,
					 0f,
					 new Vector2(0, 0),
					 1f,
					 SpriteEffects.None,
					 Converter.LayerToFloat(Layer.MasterText));
			}

			if (currentSelectionUserActor != null)
			{
				spriteBatch.DrawString(SpriteManager.Fonts["Fonts/ContextMenuFont"],
					 currentTargetUserActor.Stats.HP.ToString(),
					 new Vector2(ScreenCentre.X / 2, ScreenCentre.Y + (ScreenCentre.Y / 2)),
					 Color.Black,
					 0f,
					 new Vector2(0, 0),
					 1f,
					 SpriteEffects.None,
					 Converter.LayerToFloat(Layer.MasterText));
			}
		}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

		public void OnSelectionHightlighted(object sender, OnHighlightEventArgs e)
		{
			var userActor = sender as UserActorWithStats;

			if (currentSelectionUserActor != userActor)
			{
				currentSelectionUserActor = userActor;
				UpdateStats();
			}
		}

		public void OnTargetHightlighted(object sender, OnHighlightEventArgs e)
		{
			var userActor = sender as UserActorWithStats;

			if (currentTargetUserActor != userActor)
			{
				currentTargetUserActor = userActor;
				UpdateStats();
			}
		}
		private void UpdateStats()
		{
		}
    }
}
