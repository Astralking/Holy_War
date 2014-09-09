using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Options;

namespace Holy_War.Overlay
{
	public class Overlay
	{
		internal Point ScreenCentre { get; private set; }
		internal Point TextPosition { get; private set; }
 
		public Overlay()
		{
            ScreenCentre = new Point(MainGame.GameOptions.ResolutionX / 2, MainGame.GameOptions.ResolutionX / 2);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{

		}

		public virtual void Update(GameTime gameTime)
		{

		}
	}
}
