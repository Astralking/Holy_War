using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Worlds;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Managers
{
    public static class SpriteManager
    {
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void InitialiseTextures(World world, ContentManager contentManager)
        {
            foreach (var textureString in world.TextureNames)
                Textures.Add(textureString, contentManager.Load<Texture2D>(textureString));
        }

        public static void InitialiseFonts(List<string> fontList, ContentManager contentManager)
        {
            foreach (var fontString in fontList)
                Fonts.Add(fontString, contentManager.Load<SpriteFont>(fontString));
        }

        public static void UnloadTextures(ContentManager contentManager)
        {
            contentManager.Unload();
        }
    }
}
