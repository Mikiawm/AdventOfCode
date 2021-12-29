using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day3
{
    public static class Day3
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day3/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var bitLength = fileLines.First().Length;
            var gammaRate = "";
            var epsilonRate = "";

            for (int i = 0; i < bitLength; i++)
            {
                var countOf0Bits = fileLines.Count(x => x[i] == '0');
                var countOf1Bits = fileLines.Count(x => x[i] == '1');

                if (countOf0Bits > countOf1Bits)
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
                else
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
            }

            var gammaValue = Convert.ToInt32(gammaRate, 2);
            var epsilonValue = Convert.ToInt32(epsilonRate, 2);
            return gammaValue * epsilonValue;

        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day3/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var bitLength = fileLines.First().Length;

            var oxygenGeneratorRating = GetOxygenGeneratorRating(bitLength, fileLines);
            var co2ScrubberRating = GetCO2ScrubberRating(bitLength, fileLines);

            return Convert.ToInt32(co2ScrubberRating, 2) * Convert.ToInt32(oxygenGeneratorRating, 2);
        }

        private static string GetOxygenGeneratorRating(int bitLength, List<string> fileLines)
        {
            var newFileLines = fileLines;
            for (var i = 0; i < bitLength; i++)
            {
                var countOf0Bits = newFileLines.Count(x => x[i] == '0');
                var countOf1Bits = newFileLines.Count(x => x[i] == '1');

                if (countOf0Bits > countOf1Bits)
                {
                    newFileLines = newFileLines.FindAll(x => x[i] == '0');
                }
                else
                {
                    newFileLines = newFileLines.FindAll(x => x[i] == '1');
                }

                if (newFileLines.Count == 1)
                {
                    return newFileLines.First();
                }
            }

            return "";
        }
        private static string GetCO2ScrubberRating(int bitLength, List<string> fileLines)
        {
            var newFileLines = fileLines;
            for (var i = 0; i < bitLength; i++)
            {
                var countOf0Bits = newFileLines.Count(x => x[i] == '0');
                var countOf1Bits = newFileLines.Count(x => x[i] == '1');

                if (countOf1Bits < countOf0Bits)
                {
                    newFileLines = newFileLines.FindAll(x => x[i] == '1');
                }
                else
                {
                    newFileLines = newFileLines.FindAll(x => x[i] == '0');
                }

                if (newFileLines.Count == 1)
                {
                    return newFileLines.First();
                }

            }

            return "";
        }
    }
}