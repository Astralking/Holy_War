using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Holy_War.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Menus
{
    public class MenuActionSection : MenuSection
    {
        private readonly Vector2 _textOffset = new Vector2(50, 0);
        private readonly SelectionArrow _selectionArrow;

        public IMenuAction MenuAction { get; private set; }
        public bool Selected { get; private set; }

        public MenuActionSection(IMenuAction menuAction, Texture2D texture, Vector2 position, Layer layer, float height, float width)
            : base(texture, position, layer, height, width)
        {
            MenuAction = menuAction;
            _selectionArrow = new SelectionArrow(SpriteManager.Textures["Menus/SelectionArrow"], Layer.MasterText);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {          
            spriteBatch.DrawString(SpriteManager.Fonts["Fonts/ContextMenuFont"],
                MenuAction.DisplayText,
                PositionInPixels + _textOffset,
                Color.Black,
                0f,
                new Vector2(0,0),
                1f,
                SpriteEffects.None,
                Converter.LayerToFloat(Layer.MasterText));

            if(Selected)
                _selectionArrow.Draw(spriteBatch, PositionInPixels);

            base.Draw(spriteBatch);
        }

        public void Select()
        {
            Selected = true;
        }

        public void Deselect()
        {
            Selected = false;
        }
    }
}
