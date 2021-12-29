using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day10
{
    public class Day10
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day10/input.txt");

            var fileLines = fileReader.GetFileLines().Select(int.Parse).OrderBy(x => x);

            var joltageDifference = new List<int>()
            {
                3
            };
            var lastJoltage = 0;

            foreach (var jolts in fileLines)
            {
                joltageDifference.Add(jolts - lastJoltage);
                lastJoltage = jolts;
            }

            var oneJoltsDifference = joltageDifference.Count(x => x == 1);
            var threeJoltsDifference = joltageDifference.Count(x => x == 3);
            return oneJoltsDifference * threeJoltsDifference;
        }

        public static double GetResultPart2()
        {
            var fileReader = new FileReader("Day10/input.txt");

            var fileLines = fileReader.GetFileLines().Select(int.Parse).OrderBy(x => x).ToList();
            fileLines.Insert(0, 0);
            fileLines.Add(fileLines.Last() + 3);

            int POW2 = 0;
            int POW7 = 0;
            for (int i = 1; i < fileLines.Count-1; i++)
            {
                long negative3 = (i >= 3) ? fileLines[i - 3] : -9999;
                var next = fileLines[i + 1];
                var last = fileLines[i - 1];
                if (next - negative3 == 4)
                {
                    POW7 += 1;
                    POW2 -= 2;
                }
                else if (next - last == 2)
                {
                    POW2 += 1;
                }
            }


            return Math.Pow(2, POW2) * Math.Pow(7, POW7);
        }

        public static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
        {
            if (!source.Any())
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

            var element = source.Take(1);

            var haveNots = SubSetsOf(source.Skip(1));
            var haves = haveNots.Select(set => element.Concat(set));

            return haves.Concat(haveNots);
        }
    }
}