using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Worlds;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Managers
{
    public static class TextureManager
    {
        private static ContentManager _contentManager;
        
        public static Dictionary<string, Texture2D> Texture = new Dictionary<string, Texture2D>();
        
        public static void InitialiseTextures(World world, ContentManager contentManager)
        {
            _contentManager = contentManager;

            foreach (var textureString in world.TextureNames)
                Texture.Add(textureString, _contentManager.Load<Texture2D>(textureString));
        }
    }
}
