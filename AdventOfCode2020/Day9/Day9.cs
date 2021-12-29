using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day9
{
    public class Day9
    {
        public static long GetResults(bool isPart2 = false)
        {
            var fileReader = new FileReader("Day9/input.txt");

            var getFileLines = fileReader.GetFileLines();

            var numbers = getFileLines.Select(long.Parse).ToList();
            var preambleCount = 25;

            for (int i = preambleCount; i < numbers.Count(); i++)
            {
                var currentNumber = numbers[i];

                var preambleNumbers = numbers.GetRange(i - preambleCount, preambleCount);

                if (!CurrentNumber(preambleNumbers, currentNumber))
                {
                    return isPart2 ? SumOfConigousNumber(i, numbers, currentNumber) : currentNumber;
                }
            }

            return 0;
        }

        private static long SumOfConigousNumber(int i, List<long> numbers, long currentNumber)
        {
            for (int j = 0; j < i; j++)
            {
                var sumOfContiguousNumbers = new List<long> {numbers[j]};
                var index = j + 1;
                while (sumOfContiguousNumbers.Sum() < currentNumber)
                {
                    sumOfContiguousNumbers.Add(numbers[index]);
                    if (sumOfContiguousNumbers.Sum() == currentNumber)
                    {
                        return sumOfContiguousNumbers.Min() + sumOfContiguousNumbers.Max();
                    }

                    index++;
                }
            }

            return 0;
        }

        private static bool CurrentNumber(List<long> preambleNumbers, long currentNumber)
        {
            for (int j = 0; j < preambleNumbers.Count - 1; j++)
            {
                for (int k = j + 1; k < preambleNumbers.Count; k++)
                {
                    if (currentNumber == preambleNumbers[j] + preambleNumbers[k])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}