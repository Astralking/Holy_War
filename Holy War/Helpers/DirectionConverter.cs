using Holy_War.Enumerations;
using Microsoft.Xna.Framework;

namespace Holy_War.Helpers
{
    public static class DirectionConverter
    {
        public static Point OrientationToDirection(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return new Point(0, -1);
                    break;
                case Orientation.South:
                    return new Point(0, 1);
                    break;
                case Orientation.East:
                    return new Point(-1, 0);
                    break;
                case Orientation.West:
                    return new Point(1, 0);
                    break;
            }
    
            return new Point(0,0);
        }
    }
}
