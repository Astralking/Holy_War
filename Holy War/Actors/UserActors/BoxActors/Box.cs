using System;
using Holy_War.Enumerations;
using Holy_War.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors.BoxActors
{
	public class Box : UserActor
	{
		public EventHandler<OnHighlightEventArgs> OnHighlight;

		public Box(Texture2D texture, Point location, Layer layer)
			: base(texture, location, layer)
		{
		}

		protected virtual void OnHighlighted(OnHighlightEventArgs e)
		{
			if(OnHighlight != null)
				OnHighlight(this, e);
		}
	}
}
