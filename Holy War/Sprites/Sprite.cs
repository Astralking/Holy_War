using System;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Sprites
{
	public class Sprite
	{
		public Sprite(Texture2D texture, Layer layer)
		{
			Texture = texture;
			Layer = layer;
		}

		public Texture2D Texture { get; private set; }
		public Layer Layer { get; private set; }

        internal static Texture2D GetTeamTexture(string textureName, Team team)
        {
            return SpriteManager.Textures[textureName + Enum.GetName(typeof(Team), team)];
        }
    }
}
