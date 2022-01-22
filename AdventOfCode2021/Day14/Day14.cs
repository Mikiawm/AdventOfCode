using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day14
{
    public static class Day14
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day14/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var code = fileLines.First();

            var polymerizations = fileLines.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x => x.First(), x => x.Last());
            for (int i = 0; i < Steps; i++)
            {
                for (var index = 0; index < code.Length - 1; index++)
                {
                    char[] letters = {code[index], code[index + 1]};

                    var lettersPair = new string(letters);

                    if (polymerizations.TryGetValue(lettersPair, out var insert))
                    {
                        code = code.Insert(index + 1, insert);
                        index++;
                    }
                }
            }

            var result = code.GroupBy(x => x).Select(group => new {Metric = group.Key, Count = group.Count()});
            var minResult = result.Min(x => x.Count);
            var maxResult = result.Max(x => x.Count);

            return maxResult - minResult;
        }

        private const int Steps = 40;
    }
}