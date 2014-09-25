using System;
using Holy_War.Enumerations;
using Holy_War.Events;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors.BoxActors
{
	public class Box : UserActor
	{
		public virtual bool Visible 
		{ 
			get 
			{ 
				return _visible; 
			}
			set 
			{ 
				_visible = value;
 
				if(_visible)
					UpdateStartPosition();
			}
		}

		private bool _visible;

		public EventHandler<OnHighlightEventArgs> OnHighlight;

		public Box(Texture2D texture, Point location, Layer layer)
			: base(texture, location, layer)
		{
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (_visible)
			{
				var locationInPixels = Converter.GetLocationInPixels(GridLocation);

				spriteBatch.Draw(Texture,
					locationInPixels,
					null,
					Color.DarkRed,
					0f,
					new Vector2(0, 0),
					1f,
					SpriteEffects.None,
					Converter.LayerTofloat(Layer.Zones));
			}
		}

		public override void Update(GameTime gameTime)
		{
			var highlightedActor = GameScreen.CurrentWorld.GroundMapArray[GridLocation.X, GridLocation.Y] as UserActorWithStats;

			if (highlightedActor != null)
				OnHighlight(highlightedActor, new OnHighlightEventArgs(highlightedActor.Stats));
			else
				OnHighlight(null, new OnHighlightEventArgs(null));

            base.Update(gameTime);
		}

		protected virtual void OnHighlighted(OnHighlightEventArgs e)
		{
			if(OnHighlight != null)
				OnHighlight(this, e);
		}
	}
}
