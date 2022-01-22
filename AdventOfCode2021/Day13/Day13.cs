using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day13
{
    public static class Day13
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day13/input.txt");

            var folds = fileReader.GetFileLines().Where(x => x.Length > 0 && x.StartsWith('f'))
                .Select(x => x.Split(' ').Last())
                .Select(x => x.Split('='))
                .Select(x => (x.First(), int.Parse(x.Last())))
                .ToList();

            var coordinations = fileReader.GetFileLines().Where(x => x.Length > 0 && char.IsDigit(x[0]))
                .Select(coordination => new PepperPoint(coordination.Split(','))).ToList();

            foreach (var fold in folds)
            {
                coordinations = FoldCoordinations(fold, coordinations);
            }

            var maxX = coordinations.Max(x => x.x);
            var maxY = coordinations.Max(x => x.y);
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX ; x++)
                {
                    if (coordinations.Any(pepperPoint => pepperPoint.x == x && pepperPoint.y == y))
                    {
                        Console.Write("x");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            return coordinations.Count;
        }

        private static string DisplayCoordinations(List<PepperPoint> coordinations)
        {
            return string.Join(", ", coordinations.Select(x => $"{x.x} {x.y}").OrderBy(x => x));
        }

        private static List<PepperPoint> FoldCoordinations((string, int) fold, List<PepperPoint> coordinations)
        {
            if (fold.Item1 == "x")
            {
                coordinations.RemoveAll(x => x.x == fold.Item2);

                var biggestX = coordinations.Max(x => x.x);

                for (var index = 0; index < coordinations.Count; index++)
                {
                    if (coordinations[index].x > fold.Item2)
                    {
                        coordinations[index] = new PepperPoint(biggestX + 1 - coordinations[index].x ,coordinations[index].y );
                    }
                }
            }
            else
            {
                coordinations.RemoveAll(x => x.y == fold.Item2);

                var biggestY = coordinations.Max(x => x.y);

                for (var index = 0; index < coordinations.Count; index++)
                {
                    if (coordinations[index].y > fold.Item2)
                    {
                        coordinations[index] = new PepperPoint(coordinations[index].x, biggestY + 1 - coordinations[index].y);
                    }
                }
            }

            coordinations = coordinations.Distinct().ToList();
            return coordinations;
        }
    }

    public class PepperPoint : IEquatable<PepperPoint>
    {
        public PepperPoint(string[] split)
        {
            x = uint.Parse(split.First());
            y = uint.Parse(split.Last());
        }

        public PepperPoint(uint pepperPointX, uint pepperPointY)
        {
            x = pepperPointX;
            y = pepperPointY;
        }

        public uint y { get; set; }

        public uint x { get; set; }

        public bool Equals(PepperPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return y == other.y && x == other.x;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PepperPoint) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(y, x);
        }
    }
}