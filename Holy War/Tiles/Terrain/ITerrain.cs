using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Tiles.Terrain
{
	public interface ITerrain : ITile
	{
		float MovementCost { get; }
	}

	public static class TerrainExtension 
	{
		public static float[,] GetWeight( this ITerrain[,] terrainArray ) 
		{
			int xDimension = terrainArray.GetLength(0);
			int yDimension = terrainArray.GetLength(1);

			var weightArray = new float[xDimension, yDimension];

			for (int i = 0; i < xDimension; i++)
			{
				for (int j = 0; j < yDimension; j++)
				{
					weightArray[i, j] = terrainArray[i, j].MovementCost;
				}
			}

			return weightArray;
		}
    }
}
