using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day15
{
    public class Day15
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day15/input.txt");

            var numbers = fileReader.GetFileText().Split(',').Select(int.Parse).ToList();
            var numbersLength = numbers.Count;

            for (int i = 0; i < 2020 - numbersLength; i++)
            {
                var lastNumber = numbers.Last();
                if (!numbers.SkipLast(1).Contains(lastNumber))
                {
                    numbers.Add(0);
                }
                else
                {
                    var numbersTemp = new List<int>(numbers);
                    numbersTemp.Reverse();
                    var lastIndexOf = numbersTemp.Skip(1).ToList().IndexOf(lastNumber) + 1;

                    numbers.Add(numbers.Count - (numbers.Count - lastIndexOf));
                }
            }


            return numbers.Last();
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day15/input.txt");

            var numbers = fileReader.GetFileText().Split(',').Select(int.Parse).ToList();
            var numbersLength = numbers.Count();
            var lastNumber = numbers.Last();
            var numberDict = numbers
                .Select((s, i) => new {s, i})
                .ToDictionary(x => x.s, x => new List<int>
                {
                    x.i + 1
                });

            for (int i = numbersLength + 1; i <= 30000000; i++)
            {
                var tempList = numberDict[lastNumber];
                var reversedList = Enumerable.Reverse(tempList).ToList();

                if (reversedList.Count() == 2)
                {
                    lastNumber = reversedList.First() - reversedList.Skip(1).First();
                }
                else
                {
                    lastNumber = 0;
                }



                if (numberDict.ContainsKey(lastNumber))
                {
                    var lastElement = numberDict[lastNumber].Last();
                    numberDict[lastNumber] = new List<int>
                    {
                        lastElement, i
                    };
                }
                else
                {
                    numberDict[lastNumber] = new List<int>
                    {
                        i
                    };
                }
            }

            return lastNumber;
        }
    }
}