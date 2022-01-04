using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day11
{
    public static class Day11
    {
        private static List<Octopus> _octopusList;

        public static int GetResultParts()
        {
            var fileReader = new FileReader("Day11/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            _octopusList = fileLines.Select((fileLine, y) =>
                    fileLine.Select((item, x) => new Octopus(x, y, int.Parse(item.ToString()))))
                .SelectMany(x => x)
                .ToList();


            for (var i = 0; i < 1000; i++)
            {
                if (i == 100)
                {
                    //Part1
                    Console.WriteLine(_octopusList.First().GetFlashCount());
                }

                if (_octopusList.TrueForAll(x => x.GetFlashStatus()))
                {
                    //Part2
                    return i;
                }

                foreach (var octopus in _octopusList)
                {
                    octopus.ResetFlash();
                }

                var octopusListTemp = _octopusList;
                while (true)
                {
                    octopusListTemp = StepUpOctopusesList(octopusListTemp);

                    if (!octopusListTemp.Any())
                    {
                        break;
                    }
                }
            }

            return _octopusList.First().GetFlashCount();
        }

        private static List<Octopus> StepUpOctopusesList(List<Octopus> octopusList)
        {
            var adjacentOctopuses = new List<Octopus>();

            foreach (var octopus in octopusList)
            {
                if (octopus.StepUp())
                {
                    var adjacent = octopus
                        .GetAdjacent()
                        .Select(c => { return _octopusList.Where(n => n.GetX() == c.x && n.GetY() == c.y); })
                        .SelectMany(x => x);

                    adjacentOctopuses.AddRange(adjacent);
                }
            }

            return adjacentOctopuses;
        }
    }

    public class Octopus
    {
        private readonly int _x;
        private readonly int _y;
        private int _value;
        private bool _alreadyFleshed;
        private static int _flashCount = 0;

        public Octopus(int x, int y, int value)
        {
            _x = x;
            _y = y;
            _value = value;
        }

        public bool StepUp()
        {
            if (_value == 9 && !_alreadyFleshed)
            {
                _alreadyFleshed = true;
                _value = 0;
                _flashCount++;
                return true;
            }

            if (_value < 9 && !_alreadyFleshed)
            {
                _value++;
            }

            return false;
        }

        public IEnumerable<(int x, int y)> GetAdjacent()
        {
            return new List<(int x, int y)>
            {
                (_x - 1, _y - 1),
                (_x - 1, _y),
                (_x - 1, _y + 1),
                (_x, _y - 1),
                (_x, _y + 1),
                (_x + 1, _y - 1),
                (_x + 1, _y),
                (_x + 1, _y + 1)
            };
        }


        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public int GetFlashCount()
        {
            return _flashCount;
        }

        public void ResetFlash()
        {
            _alreadyFleshed = false;
        }

        public bool GetFlashStatus()
        {
            return _alreadyFleshed;
        }
    }
}