using System.Collections.Generic;
using System.Linq;
using Holy_War.Tiles.Terrain;
using Microsoft.Xna.Framework;

namespace Holy_War.Algorithms
{
    public static class DijkstrasAlgorithm
    {
        private static float[,] MapWeight;
        private static SortedList<double, List<Node>> _nextNodes = new SortedList<double, List<Node>>();
        private static Node[,] Graph;

        internal static Node[,] GenerateGraph(Point origin, int movement)
        {
            MapWeight = GameScreen.CurrentWorld.TerrainMapArray.GetWeight(origin, movement);

            var nodes = new Node[MapWeight.GetLength(0), MapWeight.GetLength(1)];

            for (int i = 0; i < MapWeight.GetLength(0); i++)
            {
                for (int j = 0; j < MapWeight.GetLength(1); j++)
                {
                    nodes[i, j] = new Node(new Point(i, j));
                }
            }

            for (int i = 0; i < MapWeight.GetLength(0); i++)
            {
                for (int j = 0; j < MapWeight.GetLength(1); j++)
                {
                    if (0 <= (i - 1))
                        nodes[i, j].Neighbors.Add(nodes[i - 1, j]);

                    if (MapWeight.GetLength(0) > (i + 1))
                        nodes[i, j].Neighbors.Add(nodes[i + 1, j]);

                    if (0 <= (j - 1))
                        nodes[i, j].Neighbors.Add(nodes[i, j - 1]);

                    if (MapWeight.GetLength(1) > (j + 1))
                        nodes[i, j].Neighbors.Add(nodes[i, j + 1]);
                }
            }

            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    foreach (var neighbor in nodes[i, j].Neighbors)
                    {
                        nodes[i, j].Paths.Add(new Path(nodes[i, j], neighbor, MapWeight[neighbor.Point.X, neighbor.Point.Y]));
                    }
                }
            }

            return nodes;
        }

        internal static Node[,] GenerateZone(Point origin, int movement)
        {
            Graph = GenerateGraph(origin, movement);

            var current = Graph[origin.X, origin.Y];
            current.Distance = 0;
            var unvisited = Graph.Length;

            while (current != null && unvisited > 0)
            {
                foreach (var neighbour in current.Neighbors)
                {
                    if (!neighbour.Visited)
                    {
                        var path = GetPath(current, neighbour);
                        var totalCost = current.Distance + path.Distance;

                        if (totalCost < neighbour.Distance)
                        {
                            RemoveNextNode(neighbour);
                            neighbour.Distance = totalCost;
                            neighbour.PreviousNode = current;
                            neighbour.PreviousPath = path;
                            AddNextNode(neighbour);
                        }
                    }
                }

                //mark Vertex visited
                current.Visited = true;
                RemoveNextNode(current);

                current = GetNextNode();
            }

            return Graph;
        }

        private static void AddNextNode(Node neighbour)
        {
            var cost = neighbour.Distance;
            if (_nextNodes.ContainsKey(cost))
            {
                var dist = _nextNodes[cost];
                if (dist == null)
                {
                    _nextNodes[cost] = new List<Node>()
                    {
                        neighbour
                    };
                }
                else
                {
                    dist.Add(neighbour);
                }
            }
            else
            {
                _nextNodes.Add(cost, new List<Node>()
                {
                    neighbour
                });
            }
        }

        private static Node GetNextNode()
        {
            if (_nextNodes.Count > 0)
            {
                var dist = _nextNodes.Values[0];
                return dist != null ? dist[0] : null;
            }

            return null;
        }

        private static void RemoveNextNode(Node neighbour)
        {
            var cost = neighbour.Distance;

            if (_nextNodes.ContainsKey(cost))
            {
                var dist = _nextNodes[cost];
                if (dist != null)
                {
                    dist.Remove(neighbour);
                    if (dist.Count == 0)
                    {
                        _nextNodes.Remove(cost);
                    }
                }
            }
        }

        private static Path GetPath(Node current, Node neighbour)
        {
            return current.Paths.Single(path => path.Current == current && path.Neighbour == neighbour);
        }

        //foreach (var neighbor in graph[origin.X, origin.Y].Neighbors)
        //{
        //    neighbour.Distance = graph[origin.X, origin.Y] + MapWeight[neighbour.Point.X, neighbour.Point.Y];
        //}

        //for (int i = 0; i < graph.GetLength(0); i++)
        //{
        //    for (int j = 0; j < graph.GetLength(1); j++)
        //    {
        //        foreach (var neighbor in graph[i, j].Neighbors)
        //        {
        //            neighbor.Visited = true;
        //        }
        //    }
        //}
    }

    internal class Node
    {
        public Node(Point point)
        {
            Point = point;

            Distance = float.MaxValue;
            Neighbors = new List<Node>();
            Paths = new List<Path>();
        }

        public Point Point { get; set; }
        public float Distance { get; set; }
        public bool Visited { get; set; }
        public Node PreviousNode { get; set; }
        public Path PreviousPath { get; set; }
        public List<Node> Neighbors { get; set; }
        public List<Path> Paths { get; set; }
    }

    internal class Path
    {
        public Path(Node current, Node neighbour, float distance)
        {
            Current = current;
            Neighbour = neighbour;
            Distance = distance;
        }

        public Node Current { get; set; }
        public Node Neighbour { get; set; }
        public float Distance { get; set; }
    }

}
