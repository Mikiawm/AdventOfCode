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

        public static long GetResultPart2()
        {
            var fileReader = new FileReader("Day14/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var code = fileLines.First();

            var rules = fileLines.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x => x.First(), x => x.Last());
            var pairs = new Dictionary<string, long>();
            Init(code, pairs);
            for (var i = 0; i < Steps; i++)
            {
                pairs = Step(pairs, rules);
            }

            var letterDictionary = new Dictionary<string, long>
            {
                {code[0].ToString(), 1L}
            };
            foreach (var pair in pairs)
            {
                var keys = pair.Key.ToCharArray();
                AddOrUpdate(letterDictionary, keys[1].ToString(), pair.Value);
            }

            var minResult = letterDictionary.Min(x => x.Value);
            var maxResult = letterDictionary.Max(x => x.Value);

            return maxResult - minResult;
        }

        private static void Init(string code, Dictionary<string, long> pairs)
        {
            for (var index = 0; index < code.Length - 1; index++)
            {
                char[] letters = {code[index], code[index + 1]};

                var lettersPair = new string(letters);
                AddOrUpdate(pairs, lettersPair);
            }
        }

        private static void AddOrUpdate(Dictionary<string, long> pairs, string lettersPair, long value = 1)
        {
            var containsKey = pairs.ContainsKey(lettersPair);

            if (containsKey)
            {
                pairs[lettersPair] += value;
            }
            else
            {
                pairs[lettersPair] = value;
            }
        }

        private static Dictionary<string, long> Step(Dictionary<string, long> pairs, Dictionary<string, string> rules)
        {
            var newPairs = new Dictionary<string, long>();
            foreach (var rule in rules)
            {
                var newPair1 = $"{rule.Key[0]}{rule.Value}";
                var newPair2 = $"{rule.Value}{rule.Key[1]}";

                AddOrUpdate(newPairs, newPair1, pairs.ContainsKey(rule.Key) ? pairs[rule.Key] : 0);
                AddOrUpdate(newPairs, newPair2, pairs.ContainsKey(rule.Key) ? pairs[rule.Key] : 0);
            }

            return newPairs;
        }
    }
}