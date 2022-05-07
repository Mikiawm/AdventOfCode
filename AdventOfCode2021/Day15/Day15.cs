using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day15
{
    public static class Day15
    {
        public static int XLength { get; set; }
        public static int YLength { get; set; }

        public static int GetResultPart1AStar()
        {
            var graph = new List<Tile>();
            var fileReader = new FileReader("Day15/input.txt");

            YLength = fileReader.GetFileLines().Count();
            XLength = YLength;
            for (int i = 0; i < YLength; i++)
            {
                var row = fileReader.GetFileLines().ToList()[i];
                for (int j = 0; j < row.Length; j++)
                {
                    var value = row[j] - '0';
                    graph.Add(new Tile(i, j, value));
                }
            }


            var start = graph.First();
            var end = graph.Last();

            var openList = new PriorityQueue<Tile, int>();
            openList.Enqueue(start, CitiBlock((start.X, start.Y), (XLength, YLength)));
            var closedList = new List<Tile>();
            var g = graph.ToDictionary(x => x, _ => int.MaxValue);
            g[start] = 0;

            while (openList.Count > 0)
            {
                var currentNode = openList.Dequeue();


                if (currentNode.Equals(end))
                {
                    var positions = new List<Position>();
                    var weight = 0;
                    while (currentNode.Parent != null)
                    {
                        positions.Add(new Position(currentNode.X, currentNode.Y));
                        currentNode = currentNode.Parent;
                        weight += currentNode.Weight;
                    }

                    positions.Add(new Position(currentNode.X, currentNode.Y));
                    positions.Reverse();
                    DisplayMaze(positions);
                    return weight;
                }

                if (closedList.Contains(currentNode)) continue;
                var neighbors = GetNeighbors(currentNode, graph);
                foreach (var neighbor in neighbors)
                {
                    if (closedList.Contains(neighbor)) continue;

                    var alt = g[currentNode] + neighbor.Weight;
                    if (g[neighbor] >= alt)
                    {
                        g[neighbor] = alt;
                        neighbor.Parent = currentNode;
                        neighbor.SetF(CitiBlock((neighbor.X, neighbor.Y), (XLength, YLength)) + alt);
                        openList.Enqueue(neighbor, CitiBlock((neighbor.X, neighbor.Y), (XLength, YLength)) + alt);
                    }
                }

                closedList.Add(currentNode);
            }

            return 0;
        }

        private static int CitiBlock((int x, int y) first, (int x, int y) second)
        {
            return Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y);
        }

        public class Tile : IEquatable<Tile>
        {
            public Tile(int x, int y, int weight)
            {
                X = x;
                Y = y;
                Weight = weight;
            }


            public Tile Parent { get; set; }

            public int Weight { get; set; }

            public int Y { get; set; }

            public int X { get; set; }

            public int GetF()
            {
                return F;
            }

            public void SetF(int f)
            {
                F = f;
            }

            public int F { get; set; }

            public bool Equals(Tile other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Y == other.Y && X == other.X;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Tile) obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Y, X);
            }
        }

        private static IEnumerable<Tile> GetNeighbors(Tile tile, IEnumerable<Tile> graph)
        {
            return graph
                .Where(x =>
                    x.X == tile.X && x.Y == tile.Y + 1 ||
                    x.X == tile.X + 1 && x.Y == tile.Y ||
                    x.X == tile.X && x.Y == tile.Y - 1 ||
                    x.X == tile.X - 1 && x.Y == tile.Y
                );
        }

        public static int GetResultPart1BrilliantAlgorithm()
        {
            var graph = new Dictionary<Position, int>();
            var fileReader = new FileReader("Day15/input.txt");

            YLength = fileReader.GetFileLines().Count();
            for (int i = 0; i < YLength; i++)
            {
                var row = fileReader.GetFileLines().ToList()[i];
                XLength = row.Length;
                for (int j = 0; j < row.Length; j++)
                {
                    var value = row[j] - '0';
                    graph.Add(new Position(i, j), value);
                }
            }

            Dictionary<Position, Position> parents = new Dictionary<Position, Position>
            {
                {
                    new Position(0, 1), new Position(0, 0)
                },
                {
                    new Position(1, 0), new Position(0, 0)
                }
            };
            List<Position> processed = new List<Position>();
            var dist = new Dictionary<Position, int>();
            var queue = new Queue<KeyValuePair<Position, int>>();
            var source = graph.First();
            var end = graph.Last();

            dist.Add(source.Key, 0);

            foreach (var vertex in graph)
            {
                if (!Equals(vertex.Key, source.Key))
                {
                    dist.Add(vertex.Key, int.MaxValue);
                }
            }

            queue.Enqueue(graph.First());

            while (queue.Any())
            {
                var smallestVertex = queue.Dequeue();

                foreach (var neighbor in GetNeighbors(smallestVertex.Key, graph))
                {
                    // if (processed.Any(x => x.Equals(neighbor.Key)))
                    // {
                    //     continue;
                    // }

                    var alt = dist[smallestVertex.Key] + neighbor.Value;

                    if (alt < dist[neighbor.Key])
                    {
                        dist[neighbor.Key] = alt;
                        queue.Enqueue(neighbor);
                        parents[neighbor.Key] = smallestVertex.Key;
                    }
                }

                processed.Add(smallestVertex.Key);
            }

            List<Position> path = BuildPath(parents);

            DisplayMaze(path);
            return dist.First(x => x.Key.Equals(end.Key)).Value;
        }

        private static IEnumerable<KeyValuePair<Position, int>> GetNeighbors(Position node, Dictionary<Position, int> dict)
        {
            return dict
                .Where(x =>
                    x.Key.Equals(new Position(node.X, node.Y + 1)) ||
                    x.Key.Equals(new Position(node.X + 1, node.Y)) ||
                    x.Key.Equals(new Position(node.X, node.Y - 1)) ||
                    x.Key.Equals(new Position(node.X - 1, node.Y))
                );
        }


        public static int GetResultPart1()
        {
            var graph = new Dictionary<Position, int>();
            var fileReader = new FileReader("Day15/input.txt");
            YLength = fileReader.GetFileLines().Count();
            for (int i = 0; i < YLength; i++)
            {
                var row = fileReader.GetFileLines().ToList()[i];
                XLength = row.Length;
                for (int j = 0; j < row.Length; j++)
                {
                    var value = row[j] - '0';
                    graph.Add(new Position(i, j), value);
                }
            }

            var costs = new Dictionary<Position, int>
            {
                {
                    new Position(0, 1), graph[new Position(0, 1)]
                },
                {
                    new Position(1, 0), graph[new Position(1, 0)]
                }
            };


            Dictionary<Position, Position> parents = new Dictionary<Position, Position>
            {
                {
                    new Position(0, 1), new Position(0, 0)
                },
                {
                    new Position(1, 0), new Position(0, 0)
                }
            };
            List<Position> processed = new List<Position>();
            Position node = FindLowestCostNode(costs, processed);

            while (node != null)
            {
                var cost = costs[node];
                var neighbors = GetNeighbors(graph, node);

                foreach (var n in neighbors)
                {
                    var newCost = cost + n.Value;
                    if (!costs.ContainsKey(n.Key)) costs.Add(n.Key, int.MaxValue);
                    if (costs[n.Key] > newCost)
                    {
                        costs[n.Key] = newCost;
                        parents[n.Key] = node;
                    }
                }


                processed.Add(node);
                if (node.X == XLength - 1 && node.Y == YLength - 1)
                {
                    break;
                }

                node = FindLowestCostNode(costs, processed);
            }

            Console.WriteLine(parents.Count);
            // List<Position> path = BuildPath(parents);
            //
            // DisplayMaze(path);

            return costs.Last().Value;
        }

        private static List<Position> BuildPath(Dictionary<Position, Position> parents)
        {
            List<Position> path;
            path = new List<Position> {parents.Last().Key};
            Console.WriteLine($"XLength: {XLength} YLength: {YLength}");
            var lastParent = parents.First(x => x.Key.X == XLength - 1 && x.Key.Y == YLength - 1).Key;

            path.Add(lastParent);
            while (lastParent != null && !lastParent.Equals(new Position(0, 0)))
            {
                if (!parents.ContainsKey(lastParent))
                {
                    break;
                }

                lastParent = parents[lastParent];
                path.Add(lastParent);
            }

            return path;
        }

        private static void DisplayMaze(List<Position> positions)
        {
            var numberList = new List<int>();
            var fileReader = new FileReader("Day15/input.txt");
            for (int i = 0; i < fileReader.GetFileLines().Count(); i++)
            {
                var row = fileReader.GetFileLines().ToList()[i];
                for (int j = 0; j < row.Length; j++)
                {
                    var value = row[j] - '0';

                    if (positions.Any(x => x.Y == j && x.X == i))
                    {
                        numberList.Add(value);
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(value);

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine(string.Join(",", numberList));
        }

        private static IEnumerable<KeyValuePair<Position, int>> GetNeighbors(Dictionary<Position, int> graph, Position node)
        {
            return graph
                .Where(x =>
                    x.Key.Equals(new Position(node.X, node.Y + 1)) ||
                    x.Key.Equals(new Position(node.X + 1, node.Y)) ||
                    x.Key.Equals(new Position(node.X, node.Y - 1)) ||
                    x.Key.Equals(new Position(node.X - 1, node.Y))
                );
        }

        private static Position FindLowestCostNode(Dictionary<Position, int> costs, List<Position> processed)
        {
            var lowestCost = int.MaxValue;
            Position lowestCostNode = null;
            foreach (var key in costs.Keys.Except(processed))
            {
                var cost = costs[key];
                if (cost < lowestCost)
                {
                    lowestCost = cost;
                    lowestCostNode = costs.First(x => x.Key.Equals(key)).Key;
                }
            }

            return lowestCostNode;
        }
    }


    public class Position : IEqualityComparer<Position>, IEquatable<Position>
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Position x, Position y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Position obj)
        {
            return HashCode.Combine(obj.X, obj.Y);
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}