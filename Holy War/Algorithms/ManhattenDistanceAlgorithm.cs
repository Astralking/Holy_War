using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Algorithms
{
    public static class ManhattenDistanceAlgorithm
    {
        internal static int[,] GenerateGrid(int originX, int originY, int distance)
        {
            var grid = new int[GameScreen.CurrentWorld.WidthInTiles, GameScreen.CurrentWorld.HeightInTiles];

            for (var i = originX - distance; i <= originX + distance; ++i)
            {
                for (var j = originY - distance; j <= originY + distance; ++j)
                {
                    if (i < 0 || j < 0 || i >= GameScreen.CurrentWorld.WidthInTiles || j >= GameScreen.CurrentWorld.HeightInTiles)
                        continue;

                    if (Math.Abs(originX - i) + Math.Abs(originY - j) > distance)
                        continue;

                    if (i == originX && j == originY)
                        continue;

                    grid[i, j] = 1;
                }
            }

            return grid;
        }
    }
}
