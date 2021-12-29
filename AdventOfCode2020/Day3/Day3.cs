using System;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day3
{
    public static class Day3
    {
        public static int GetResultPart1(int x, int y)
        {
            var fileReader = new FileReader("Day3/input.txt");

            var mapOfTrees = fileReader.GetFileLines()
                .Select(x => string.Join("", Enumerable.Repeat(x, 10000)).ToCharArray()).ToList();

            var currentCoordinationX = 0;
            var currentCoordinationY = 0;

            var currentTrees = 0;
            while (true)
            {
                try
                {
                    currentCoordinationX += x;
                    currentCoordinationY += y;
                    if (mapOfTrees[currentCoordinationY][currentCoordinationX] == '#')
                    {
                        mapOfTrees[currentCoordinationY][currentCoordinationX] = 's';
                        currentTrees++;
                    }
                }
                catch (Exception e)
                {
                    return currentTrees;
                }
            }
        }

        public static long GetResultPart2()
        {
            // GetResultPart1(1, 1) * GetResultPart1(3, 1) * GetResultPart1(5, 1) * GetResultPart1(7, 1) *
            return GetResultPart1(1, 1) * GetResultPart1(3, 1) * GetResultPart1(5, 1) * GetResultPart1(7, 1) *
                   GetResultPart1(1, 2);
        }
    }
}
