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

        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day15/input.txt");

            var data = fileReader.GetFileLines().ToList();

            var dist = GetShortestPathAStar(data);

            return dist.First(x => x.Key.Equals((XLength - 1, YLength - 1))).Value;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day15/input.txt");

            var data = fileReader.GetFileLines().ToList();

            var newData = new List<string>();

            for (int i = 0; i < data.Count * 5; i++)
            {
                var newLine = "";
                for (int j = 0; j < data.Count * 5; j++)
                {
                    var newI = i % data.Count;
                    var newJ = j % data.Count;
                    var newValue = data[newI][newJ] - '0' + i / data.Count + j / data.Count;

                    if (newValue > 9)
                    {
                        newValue %= 10;
                        newValue += 1;
                    }

                    newLine += newValue.ToString();
                }

                newData.Add(newLine);
            }

            var dist = GetShortestPathAStar(newData);


            return dist.First(x => x.Key.Equals((XLength - 1, YLength - 1))).Value;
        }

        private static Dictionary<(int, int), int> GetShortestPathAStar(IReadOnlyList<string> data)
        {
            XLength = YLength = data.Count;

            var parents = new Dictionary<(int, int), (int, int)>();
            var closedList = new List<(int, int)>();
            var dist = new Dictionary<(int, int), int>();
            for (var i = 0; i < XLength; i++)
            {
                for (var j = 0; j < YLength; j++)
                {
                    dist.Add((i, j), int.MaxValue);
                }
            }

            dist.AddOrReplace((0, 0), 0);

            var openList = new PriorityQueue<(int, int), int>();

            openList.Enqueue((0, 0), 0);

            while (openList.Count > 0)
            {
                var node = openList.Dequeue();

                if (node.Item1 == XLength - 1 && node.Item2 == YLength - 1)
                {
                    DisplayMaze(BuildPath(parents), data);
                    return dist;
                }

                if (closedList.Contains(node))
                {
                    continue;
                }

                foreach (var neighbor in GetNeighbors(node))
                {
                    if (closedList.Contains(neighbor)) continue;

                    var alt = dist[node] + (data[neighbor.x][neighbor.y] - '0');

                    if (alt < dist[neighbor])
                    {
                        dist.AddOrReplace(neighbor, alt);
                        openList.Enqueue(neighbor, CitiBlock((neighbor.x, neighbor.y), (XLength, YLength)) + alt);
                        parents.AddOrReplace(neighbor, node);
                    }
                }

                closedList.Add(node);
            }

            return dist;
        }

        private static IEnumerable<(int x, int y)> GetNeighbors((int x, int y) node)
        {
            var x = node.x;
            var y = node.y;
            var possibleNeighbours = new[]
            {
                (x + 1, y), (x - 1, y), (x, y + 1), (x, y - 1)
            };
            return possibleNeighbours
                .Where(item =>
                    item.Item1 >= 0 && item.Item2 >= 0 && item.Item1 < XLength && item.Item2 < YLength
                );
        }

        private static int CitiBlock((int x, int y) first, (int x, int y) second)
        {
            return Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y);
        }

        private static void DisplayMaze(List<(int, int)> positions, IReadOnlyList<string> data)
        {
            var numberList = new List<int>();

            for (int i = 0; i < data.Count; i++)
            {
                var row = data.ToList()[i];
                for (int j = 0; j < row.Length; j++)
                {
                    var value = row[j] - '0';

                    if (positions.Any(x => x.Item2 == j && x.Item1 == i))
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

        private static List<(int, int)> BuildPath(Dictionary<(int, int), (int, int)> parents)
        {
            var path = new List<(int, int)> {parents.Last().Key};
            Console.WriteLine($"XLength: {XLength} YLength: {YLength}");
            var lastParent = parents.First(x => x.Key.Item1 == XLength - 1 && x.Key.Item2 == YLength - 1).Key;

            path.Add(lastParent);
            while (!lastParent.Equals((0, 0)))
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
    }
}