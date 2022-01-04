using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day10
{
    public static class Day10
    {
        private static readonly Dictionary<char, char> BracketPairs = new Dictionary<char, char>
        {
            {'{', '}'},
            {'[', ']'},
            {'(', ')'},
            {'<', '>'}
        };

        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day10/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var corruptedBrackets = "";
            var bracketsCost = new Dictionary<char, int>
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };
            foreach (var fileLine in fileLines)
            {
                var bracketsStack = new Stack<char>();
                var brackets = fileLine.ToCharArray();

                corruptedBrackets += GetCorruptedBracket(brackets, bracketsStack)?.ToString();
            }

            return corruptedBrackets.ToCharArray().Select(x => bracketsCost[x]).Sum();
        }

        public static long GetResultPart2()
        {
            var fileReader = new FileReader("Day10/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var bracketsCost = new Dictionary<char, long>
            {
                {'(', 1},
                {'[', 2},
                {'{', 3},
                {'<', 4}
            };
            var results = new List<long>();
            foreach (var fileLine in fileLines)
            {
                var bracketsStack = new Stack<char>();
                var brackets = fileLine.ToCharArray();

                if (GetCorruptedBracket(brackets, bracketsStack)?.ToString() == null)
                {
                    results.Add(bracketsStack.Select(x => bracketsCost[x]).Aggregate(0L, (total, next) => total * 5 + next));
                }
            }

            return results.OrderByDescending(x => x).ToList()[results.Count / 2];
        }

        private static char? GetCorruptedBracket(IEnumerable<char> brackets, Stack<char> lastOpenBracket)
        {
            foreach (var bracket in brackets)
            {
                if (BracketPairs.Keys.Contains(bracket))
                {
                    lastOpenBracket.Push(bracket);
                    continue;
                }

                var closedBracketTuple = BracketPairs.FirstOrDefault(x => x.Value == bracket);
                if (!closedBracketTuple.Equals(default) && lastOpenBracket.Peek() == closedBracketTuple.Key)
                {
                    lastOpenBracket.Pop();
                }
                else
                {
                    return bracket;
                }
            }

            return null;
        }
    }
}