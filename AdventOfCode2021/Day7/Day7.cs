using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day7
{
    public static class Day7
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day7/input.txt");

            var crabPositions = fileReader.GetFileText().Split(",").Select(int.Parse);
            var listOfSums = new List<int>();
            for (var i = 0; i < crabPositions.Max(); i++)
            {
                var sum = 0;
                foreach (var crabPosition in crabPositions)
                {
                    sum += Math.Abs(crabPosition - i);
                }

                listOfSums.Add(sum);

            }
            return listOfSums.Min();
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day7/input.txt");

            var crabPositions = fileReader.GetFileText().Split(",").Select(int.Parse);
            var listOfSums = new List<int>();
            for (var i = 0; i < crabPositions.Max(); i++)
            {
                var sum = 0;
                foreach (var crabPosition in crabPositions)
                {
                    sum += Enumerable.Range(1, Math.Abs(crabPosition - i)).Sum();
                }

                listOfSums.Add(sum);

            }
            return listOfSums.Min();
        }
    }
}