using System;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day2
{
    public static class Day2
    {
        // 1-3 a: abcde
        // 1-3 b: cdefg
        // 2-9 c: ccccccccc
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day2/input.txt");

            var textLines = fileReader.GetFileLines();
            var result = 0;

            foreach (var textLine in textLines)
            {
                var splitTextLine = textLine.Split(' ');

                var interval = splitTextLine[0].Split('-');
                var from = interval[0];
                var to = interval[1];

                var letter = splitTextLine[1].First();

                var password = splitTextLine[2];

                var letterCount = password.ToCharArray().Count(x => x == letter);
                if (letterCount >= int.Parse(from) && letterCount <= int.Parse(to)) result++;
            }

            return result;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day2/input.txt");

            var textLines = fileReader.GetFileLines();
            var result = 0;

            foreach (var textLine in textLines)
            {
                var splitTextLine = textLine.Split(' ');

                var interval = splitTextLine[0].Split('-');
                var from = int.Parse(interval[0]);
                var to = int.Parse(interval[1]);

                var letter = splitTextLine[1].First();

                var password = splitTextLine[2];

                if (password[from - 1].Equals(letter) && !password[to - 1].Equals(letter)) result++;
                if (!password[from - 1].Equals(letter) && password[to - 1].Equals(letter)) result++;
            }

            return result;
        }
    }
}
