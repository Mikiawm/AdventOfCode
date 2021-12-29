using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day5
{
    public static class Day5
    {
        public static IEnumerable<int> GetResultsPart1()
        {
            var fileReader = new FileReader("Day5/input.txt");

            var fileLines = fileReader.GetFileLines();

            foreach (var fileLine in fileLines)
            {
                // Console.WriteLine(fileLine);
                var rows = Enumerable.Range(0, 128).ToList();
                var columns = Enumerable.Range(0, 8).ToList();
                var startRow = 0;
                var endRow = 127;
                var startColumn = 0;
                var endColumn = 7;
                var flightProcedures = fileLine.ToCharArray();
                var rowProcedures = flightProcedures.Take(7).ToArray();
                var columnProcedures = flightProcedures.Skip(7).Take(3).ToArray();

                var row = NewMethod(rowProcedures, endRow, startRow, rowProcedures, rows, 'B', 'F');
                var column = NewMethod(columnProcedures, endColumn, startColumn, columnProcedures, columns, 'R', 'L');
                yield return row * 8 + column;
            }
        }

        private static int NewMethod(IEnumerable<char> rowProcedures, int endRow, int startRow, char[] flightProcedures, List<int> rows,
            char high, char low)
        {
            foreach (var rowProcedure in rowProcedures)
            {
                if (rowProcedure == low)
                {
                    endRow = endRow - (Math.Abs((startRow - endRow)) / 2) - 1;
                }

                if (rowProcedure == high)
                {
                    startRow = startRow + Math.Abs(startRow - endRow) / 2 + 1;
                }
            }

            if (flightProcedures.TakeLast(1).First() == low)
            {
                return rows.GetRange(startRow, 1).First();
            }

            if (flightProcedures.TakeLast(1).First() == high)
            {
               return rows.GetRange(endRow, 1).First();
            }

            return 0;
        }

        public static int GetResultsPart2()
        {
            var result = GetResultsPart1();
            var results = Enumerable.Range(1, result.Max()).Except(result);
            return results.Max();
        }
    }
}
