using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day1
{
    public static class Day1
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day1/input.txt");

            var fileLines = fileReader.GetFileLines().Select(int.Parse).ToList();
            var count = 0;
            var lastNumber = fileLines.First();

            fileLines.Skip(1).ToList().ForEach(i =>
            {
                if (i > 0)
                {
                    if (i > lastNumber)
                    {
                        count++;
                    }
                }
                lastNumber = i;
            });

            return count;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day1/input.txt");

            var fileLines = fileReader.GetFileLines().Select(int.Parse).ToList();
            var count = 0;
            var lastSumOf3 = fileLines.Take(3).Sum();

            foreach (var (item, index) in fileLines.WithIndex())
            {
                try
                {
                    if(index == 0) continue;

                    var sumOf3 = item + fileLines[index + 1] + fileLines[index + 2];

                    if (sumOf3 > lastSumOf3)
                    {
                        count++;
                    }

                    lastSumOf3 = sumOf3;
                }
                catch (Exception e)
                {
                    return count;
                }
            }

            return count;

        }
    }
}