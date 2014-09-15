using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Holy_War.Tiles.Terrain
{
	public interface ITerrain : ITile
	{
		float MovementCost { get; }
	}

	public static class TerrainExtension 
	{
		public static float[,] GetWeight( this ITerrain[,] terrainArray, Point origin, int size)
		{
            var weightArray = new float[origin.X + size, origin.Y + size];

            for (int i = 0; i < origin.X + size; i++)
			{
                for (int j = 0; j < origin.Y + size; j++)
				{
					weightArray[i, j] = terrainArray[i, j].MovementCost;

				    if (i == origin.X && j == origin.Y)
				        weightArray[i, j] = 0;
				}
			}

			return weightArray;
		}
    }
}
