using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace AdventOfCode2020.Day14
{
    public static class Day14
    {
        public static long GetResultPart1()
        {
            var fileReader = new FileReader("Day14/input.txt");

            var textLines = fileReader.GetFileLines();
            var currentMask = string.Empty;
            var currentMemory = new Dictionary<int, string>();

            foreach (var textLine in textLines)
            {
                if (textLine.StartsWith("mask"))
                {
                    currentMask = textLine.Split("=").Last().Trim();
                }
                else
                {
                    var splitMemory = textLine.Split('=');
                    var memoryKey = int.Parse(new String(splitMemory.First().Where(Char.IsDigit).ToArray()));

                    var memoryBinary = Convert.ToString(int.Parse(splitMemory.Last()), 2);
                    var memoryValue = memoryBinary.PadLeft(36, '0');

                    currentMemory[memoryKey] = SetMask(currentMask, memoryValue);
                }
            }


            return currentMemory.Values.Select(x => Convert.ToInt64(x, 2)).Sum();
            ;
        }

        //Kazda wartosc do innego memory zapisac.
        public static long GetResultPart2()
        {
            var fileReader = new FileReader("Day14/input.txt");

            var textLines = fileReader.GetFileLines();
            var currentMask = string.Empty;
            var currentMemory = new Dictionary<string, long>();

            foreach (var textLine in textLines)
            {
                if (textLine.StartsWith("mask"))
                {
                    currentMask = textLine.Split("=").Last().Trim();
                }
                else
                {
                    var splitMemory = textLine.Split('=');
                    var memoryKey = int.Parse(splitMemory.Last());

                    var memoryBinary = Convert.ToString(int.Parse(new String(splitMemory.First().Where(char.IsDigit).ToArray())), 2);
                    var memoryValue = memoryBinary.PadLeft(36, '0');

                    var newValue = SetMask2(currentMask, memoryValue);

                    var permutations = GetPermutationsWithRept(new[] {0, 1}, newValue.Count(x => x == 'X'));

                    foreach (var permutation in permutations)
                    {
                        var itemToReplace = new StringBuilder(newValue);
                        foreach (var number in permutation)
                        {
                            itemToReplace.Replace('X', Convert.ToChar(number.ToString()),
                                itemToReplace.IndexOf("X", 0, true), 1);
                        }

                        currentMemory[itemToReplace.ToString()] = memoryKey;
                    }
                }
            }


            return currentMemory.Values.Sum();

        }

        private static string SetMask(string mask, string newMemory)
        {
            var returnMemory = string.Empty;
            for (int i = 0; i < newMemory.Length; i++)
            {
                if (mask[i] != 'X')
                {
                    returnMemory += mask[i];
                }
                else
                {
                    returnMemory += newMemory[i];
                }
            }

            return returnMemory;
        }

        private static string SetMask2(string mask, string newMemory)
        {
            var returnMemory = string.Empty;
            for (int i = 0; i < newMemory.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    returnMemory += 'X';
                }
                else if (mask[i] == '1')
                {
                    returnMemory += '1';
                }
                else
                {
                    returnMemory += newMemory[i];
                }
            }

            return returnMemory;
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            var enumerable = elements.ToList();
            return k == 0
                ? new[] {new T[0]}
                : enumerable.SelectMany((e, i) =>
                    enumerable.Skip(i + 1).Combinations(k - 1).Select(c => (new[] {e}).Concat(c)));
        }

        static IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] {t});
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] {t2}));
        }

        /// <summary>
        /// Returns the index of the start of the contents in a StringBuilder
        /// </summary>
        /// <param name="value">The string to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase)
        {
            int index;
            int length = value.Length;
            int maxSearchLength = (sb.Length - length) + 1;

            if (ignoreCase)
            {
                for (int i = startIndex; i < maxSearchLength; ++i)
                {
                    if (Char.ToLower(sb[i]) == Char.ToLower(value[0]))
                    {
                        index = 1;
                        while ((index < length) && (Char.ToLower(sb[i + index]) == Char.ToLower(value[index])))
                            ++index;

                        if (index == length)
                            return i;
                    }
                }

                return -1;
            }

            for (int i = startIndex; i < maxSearchLength; ++i)
            {
                if (sb[i] == value[0])
                {
                    index = 1;
                    while ((index < length) && (sb[i + index] == value[index]))
                        ++index;

                    if (index == length)
                        return i;
                }
            }

            return -1;
        }
    }
}