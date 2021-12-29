using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day13
{
    public static class Day13
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day13/input.txt");

            var textLines = fileReader.GetFileLines();

            var earliest = int.Parse(textLines.First());
            var busTimes = textLines.Last().Split(',').Where(x => x != "x").Select(int.Parse).ToList();
            var busEarliest = new List<int>();

            foreach (var busTime in busTimes)
            {
                var busTimeTemp = busTime;
                while (busTimeTemp < earliest)
                {
                    busTimeTemp += busTime;
                }

                busEarliest.Add(busTimeTemp);
            }

            var busId = busTimes[busEarliest.IndexOf(busEarliest.Min())];
            return (busEarliest.Min() - earliest) * busId;
        }

        public static long GetResultPart2()
        {
            var fileReader = new FileReader("Day13/input.txt");

            var textLines = fileReader.GetFileLines();

            var busTimes = textLines.Last().Split(',')
                .Select((x, index) => new KeyValuePair<string, int>(x, index))
                .Where(x => x.Key != "x")
                .Select(x => new KeyValuePair<int, int>(int.Parse(x.Key), x.Value));

            var busItems = busTimes.Select(x => (long)x.Key).ToArray();
            var remainders = busTimes.Select(x => (long)x.Key - (long)x.Value).ToArray();

            long result = ChineseRemainder.Solve(busItems, remainders);

            int counter = 0;
            int maxCount = busItems.Length - 1;
            while (counter <= maxCount)
            {
                Console.WriteLine($"{result} ≡ {busItems[counter]} (mod {remainders[counter]})");
                counter++;
            }

            return result;
        }
    }

}