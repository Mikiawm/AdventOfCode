using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day1
{
    public static class Day1
    {
        public static string GetResultPart1()
        {
            var fileReader = new FileReader("Day1/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var current = 1;
            foreach (var line in fileLines)
            {
                for (var i = current; i < fileLines.Count; i++)
                {
                    var sum = int.Parse(line) + int.Parse(fileLines[i]);
                    if (sum == 2020)
                    {
                        return (int.Parse(line) * int.Parse(fileLines[i])).ToString();
                    }
                }

                current++;
            }

            return "null";
        }

        public static string GetResultPart2()
        {
            var fileReader = new FileReader("Day1/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var first = 1;
            var second = 1;
            foreach (var line in fileLines)
            {
                for (var i = first; i < fileLines.Count; i++)
                {
                    for (var j = second; j < fileLines.Count; j++)
                    {
                        var sum = int.Parse(line) + int.Parse(fileLines[i]) + int.Parse(fileLines[j]);
                        if (sum == 2020)
                        {
                            return (int.Parse(line) * int.Parse(fileLines[i]) * int.Parse(fileLines[j])).ToString();
                        }
                    }
                }

                first++;
                second++;
            }

            return "null";
        }
    }
}
