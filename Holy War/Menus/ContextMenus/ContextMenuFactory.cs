using System.Collections.Generic;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;

namespace Holy_War.Menus.ContextMenus
{
    public static class ContextMenuFactory
    {
        public static ContextMenu CreateContextMenu(List<IMenuAction> menuActions, Vector2 position)
        {
            var menuSections = new List<MenuActionSection>();

            if (menuActions != null)
            {
                foreach (var menuAction in menuActions)
                {
                    menuSections.Add(
                        new MenuActionSection(
                            menuAction,
                            SpriteManager.Textures["Menus/ContextMenu/ContextMenuSection"], 
                            new Vector2(0, 0), 
                            Layer.Master,
                            32f,
                            200f));
                }
            }

            return new ContextMenu(menuSections, position);
        }
    }
}
