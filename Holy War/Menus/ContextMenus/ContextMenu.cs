using System.Collections.Generic;
using System.Linq;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Holy_War.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Menus.ContextMenus
{
    public class ContextMenu
    {
        private MenuSection _topSection;
        private MenuSection _bottomSection;
        private List<MenuActionSection> _menuActionSections;
        private int SelectedIndex;
        private bool _visible;

        public ContextMenu(List<MenuActionSection> extendableMenuSectionList, Vector2 position)
        {
            var positionInPixels = Converter.GetLocationInPixels(position);

            ConstructMenu(extendableMenuSectionList, positionInPixels);         
        }

        private void ConstructMenu(List<MenuActionSection> extendableMenuSectionList, Vector2 positionInPixels)
        {
            _topSection = new MenuSection(SpriteManager.Textures["Menus/ContextMenu/ContextMenuTop"], positionInPixels, Layer.Master, 27f, 200f);
            positionInPixels += new Vector2(0, 27f);

            _menuActionSections = extendableMenuSectionList;

            foreach (var menuActionSection in _menuActionSections)
            {
                menuActionSection.UpdatePositionByPixels(positionInPixels);
                positionInPixels += new Vector2(0, menuActionSection.Height);
            }

            _menuActionSections[SelectedIndex].Select();

            _bottomSection = new MenuSection(SpriteManager.Textures["Menus/ContextMenu/ContextMenuBottom"], positionInPixels, Layer.Master, 28f, 200f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                _topSection.Draw(spriteBatch);

                foreach (var menuActionSection in _menuActionSections)
                    menuActionSection.Draw(spriteBatch);

                _bottomSection.Draw(spriteBatch);
            }
        }

        public void Show()
        {
            _visible = true;
        }

        public void Hide()
        {
            _visible = false;
        }

        public void Move(Point direction, GameTime gameTime)
        {
            _menuActionSections[SelectedIndex].Deselect();

            if (direction.Y > 0 && SelectedIndex < _menuActionSections.Count - 1)
                SelectedIndex++;
            if (direction.Y < 0 && SelectedIndex > 0)
                SelectedIndex--;

            _menuActionSections[SelectedIndex].Select();
        }

        public IMenuAction GetMenuAction()
        {
            return _menuActionSections[SelectedIndex].MenuAction;
        }
    }
}
