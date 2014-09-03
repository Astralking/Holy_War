using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Options
{
    public class GameOptions
    {
        public int ResolutionX { get; private set; }
        public int ResolutionY { get; private set; }

        public GameOptions(int resolutionX, int resolutionY)
        {
            ResolutionX = resolutionX;
            ResolutionY = resolutionY;
        }
    }
}
