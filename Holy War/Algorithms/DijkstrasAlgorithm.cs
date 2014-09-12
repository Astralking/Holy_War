using System.Collections.Generic;
using Holy_War.Tiles.Terrain;
using Microsoft.Xna.Framework;

namespace Holy_War.Algorithms
{
	public static class DijkstrasAlgorithm
	{
		internal static Node[,] GenerateGraph(int[,] weight, Point origin, int movement)
		{
			var nodes = new Node[movement, movement];

			for (int i = 0; i < movement; i++)
			{
				for (int j = 0; j < movement; j++)
				{
					nodes[i, j] = new Node(origin.X + i, origin.Y + j);
				}
			}

			GameScreen.CurrentWorld.TerrainMapArray.GetWeight();

			for (int i = 0; i < weight.GetLength(0); i++)
			{
				for (int j = 0; j < weight.GetLength(1); j++)
				{
					if (0 <= (i - 1))
						nodes[i, j].Neighbors.Add(nodes[i - 1, j]);

					if (weight.GetLength(0) > (i + 1))
						nodes[i, j].Neighbors.Add(nodes[i + 1, j]);

					if (0 <= (j - 1))
						nodes[i, j].Neighbors.Add(nodes[i, j - 1]);

					if (weight.GetLength(1) > (j + 1))
						nodes[i, j].Neighbors.Add(nodes[i, j + 1]);
				}
			}

			return nodes;
		}
	}

	internal class Node
	{
		public Node(int xCoord, int yCoord)
		{
			XCoord = xCoord;
			YCoord = yCoord;

			Distance = int.MaxValue;
			Visited = false;
			Neighbors = new List<Node>();
		}

		public int XCoord { get; set; }
		public int YCoord { get; set; }
		public int Distance { get; set; }
		public bool Visited { get; set; }
		public List<Node> Neighbors { get; set; }
	}
}
