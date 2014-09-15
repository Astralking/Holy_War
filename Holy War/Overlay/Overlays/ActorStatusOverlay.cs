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
		private UserActorWithStats _currentSelectionUserActor;
		private UserActorWithStats _currentTargetUserActor;
		
        public override void Draw(SpriteBatch spriteBatch)
        {
			if (_currentSelectionUserActor != null)
			{
				spriteBatch.DrawString(SpriteManager.Fonts["Fonts/ContextMenuFont"],
					 _currentSelectionUserActor.Stats != null
						? "HP: " + _currentSelectionUserActor.Stats.HP.ToString() 
						: string.Empty,
					 new Vector2(ScreenCentre.X / 2, ScreenCentre.Y + (ScreenCentre.Y / 2)),
					 Color.Black,
					 0f,
					 new Vector2(0, 0),
					 1f,
					 SpriteEffects.None,
					 Converter.LayerTofloat(Layer.MasterText));
			}

			if (_currentTargetUserActor != null)
			{
				spriteBatch.DrawString(SpriteManager.Fonts["Fonts/ContextMenuFont"],
					_currentTargetUserActor.Stats != null 
						? "HP: " + _currentTargetUserActor.Stats.HP.ToString() 
						: string.Empty,
					new Vector2(ScreenCentre.X + (ScreenCentre.X / 2), ScreenCentre.Y + (ScreenCentre.Y / 2)),
					Color.Black,
					0f,
					new Vector2(0, 0),
					1f,
					SpriteEffects.None,
					Converter.LayerTofloat(Layer.MasterText));
			}
		}

		public void OnSelectionHightlighted(object sender, OnHighlightEventArgs e)
		{
			var userActor = sender as UserActorWithStats;

		    if (userActor != null)
		    {
		        if (_currentSelectionUserActor != null && _currentSelectionUserActor != userActor)
		            _currentSelectionUserActor.HighlightZone(null);
		        else
		            userActor.HighlightZone(userActor.MovementZone);

		        _currentSelectionUserActor = userActor;
		    }
		    else
		    {
		        if (_currentSelectionUserActor == null) 
                    return;

		        _currentSelectionUserActor.HighlightZone(null);
		        _currentSelectionUserActor = null;
		    }

		}

		public void OnTargetHightlighted(object sender, OnHighlightEventArgs e)
		{
			var userActor = sender as UserActorWithStats;

			_currentTargetUserActor = userActor;
		}
    }
}
