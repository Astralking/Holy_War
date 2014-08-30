using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Enumerations;
using Holy_War.Input.Commands;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Input
{
	public class InGameInputHandler : InputHandler
	{
        private Texture2D _selectionBox;

		public InGameInputHandler(SpriteBatch spriteBatch)
		{
            _up = new MoveCommand(Orientation.North);
            _down = new MoveCommand(Orientation.South);
            _left = new MoveCommand(Orientation.West);
            _right = new MoveCommand(Orientation.East);

            _selectionBox = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            var data = new Color[Tile.GetDefaultTileWidth() * Tile.GetDefaultTileHeight()];
            for (int i = 0; i < data.Length / 2; ++i)
                data[i] = Color.Black;

            _selectionBox.SetData(data);
		}

        public void DrawSelection(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_selectionBox, 
                new Rectangle(0,0,Tile.GetDefaultTileWidth(), Tile.GetDefaultTileHeight()), 
                null, 
                Color.Black, 
                0f, 
                new Vector2(0,0), 
                SpriteEffects.None, 
                Sprite.Sprite.LayerToFloat(Layer.Master));
        }
	}
}
