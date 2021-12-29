using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day5
{
    public static class Day5
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day5/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var vents = GetVents(fileLines);

            var lineVents = vents.Where(vent => vent.IsLine());

            var allLinePoints = new List<Point>();
            foreach (var lineVent in lineVents)
            {
                var allVentPoints = lineVent.GetAllPoints();
                allLinePoints.AddRange(allVentPoints);
            }

            return CountOverlap(allLinePoints);
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day5/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var vents = GetVents(fileLines);
            var lineVents = vents.Where(vent => vent.IsLine());

            var diagonalVents = vents.Where(x => !x.IsLine() && x.GetDiagonalLines());

            var allLinePoints = new List<Point>();
            foreach (var lineVent in lineVents)
            {
                var allVentPoints = lineVent.GetAllPoints();
                allLinePoints.AddRange(allVentPoints);
            }

            foreach (var diagonalVent in diagonalVents)
            {
                var diagonalPoints = diagonalVent.GetDiagonalLinePoints();
                allLinePoints.AddRange(diagonalPoints);
            }

            return CountOverlap(allLinePoints);
        }

        private static List<Vent> GetVents(List<string> fileLines)
        {
            var vents = new List<Vent>();

            foreach (var fileLine in fileLines)
            {
                var ventCoordination = fileLine.Split(" -> ");
                vents.Add(new Vent(ventCoordination.First(), ventCoordination.Last()));
            }

            return vents;
        }

        private static int CountOverlap(List<Point> allLinePoints)
        {
            return allLinePoints.GroupBy(x => new
            {
                x.y,
                x.x
            }).Select(group => new
            {
                Point = @group.Key,
                Count = @group.Count()
            }).Count(x => x.Count > 1);
        }
    }

    public class Vent
    {
        private readonly Point _from;
        private readonly Point _to;

        public Vent(string from, string to)
        {
            _from = new Point(from.Split(","));
            _to = new Point(to.Split(","));
        }

        public bool IsLine()
        {
            return _from.x == _to.x || _from.y == _to.y;
        }

        public IEnumerable<Point> GetAllPoints()
        {
            var allPoints = new List<Point>();

            for (var y = _from.y; y <= _to.y; y++)
            {
                for (var x = _from.x; x <= _to.x; x++)
                {
                    allPoints.Add(new Point(x, y));
                }
            }

            for (var y = _to.y; y <= _from.y; y++)
            {
                for (var x = _to.x; x <= _from.x; x++)
                {
                    allPoints.Add(new Point(x, y));
                }
            }

            return allPoints;
        }


        public bool GetDiagonalLines()
        {
            var angle = Math.Atan((_from.y - _to.y) / (_from.x - _to.x));
            angle = angle * 180 / Math.PI;
            return Math.Abs(angle) - 45d < 0.01;
        }

        public IEnumerable<Point> GetDiagonalLinePoints()
        {
            var xRange = IEnumerable.XRange(_from.x, _to.x);
            var yRange = IEnumerable.XRange(_from.y, _to.y);

            return xRange.Zip(yRange, (x, y) => new Point(x, y)).ToList();
        }
    }

    public class Point
    {
        public int x;
        public int y;

        public Point(string[] split)
        {
            x = int.Parse(split.First());
            y = int.Parse(split.Last());
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}