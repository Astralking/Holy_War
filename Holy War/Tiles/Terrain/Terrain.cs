using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Tiles.Terrain
{
	public class Terrain : Tile
	{
		internal float _movementCost;

		public Terrain(Texture2D texture, Point location, Layer layer, float movementCost)
			: base(texture, location, layer)
		{
			_movementCost = movementCost;
		}

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
		{
		}
	}
}
