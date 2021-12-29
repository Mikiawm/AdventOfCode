using System.Linq;
using Common;

namespace AdventOfCode2021.Day2
{
    public static class Day2
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day2/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var depth = 0;
            var horizontal = 0;

            foreach (var strings in fileLines.Select(fileLine => fileLine.Split()))
            {
                switch (strings.First())
                {
                    case "forward":
                        horizontal += int.Parse(strings.Last());
                        break;
                    case "down":
                        depth += int.Parse(strings.Last());
                        break;
                    case "up":
                        depth -= int.Parse(strings.Last());
                        break;
                }
            }

            return depth * horizontal;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day2/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var depth = 0;
            var horizontal = 0;
            var aim = 0;

            foreach (var strings in fileLines.Select(fileLine => fileLine.Split()))
            {
                switch (strings.First())
                {
                    case "forward":
                        var x = int.Parse(strings.Last());
                        horizontal += x;
                        depth += x * aim;
                        break;
                    case "down":
                        aim += int.Parse(strings.Last());
                        break;
                    case "up":
                        aim -= int.Parse(strings.Last());
                        break;
                }
            }

            return depth * horizontal;
        }
    }
}