using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day11
{
    public class Day11
    {
        public static int GetResultPart1()
        {
            return GetResult(CheckNeighbors, 4);
        }
        public static int GetResultPart2()
        {
            return GetResult(CheckNeighborsFirstInSight, 5);
        }

        public static int GetResult(Func<int, int, List<List<char>>, int, int, char> checkNeighbors, int makeEmpty)
        {
            var fileReader = new FileReader("Day11/input.txt");

            var fileLines = fileReader.GetFileLines();

            var grid = fileLines.Select(x => x.ToCharArray().ToList()).ToList();
            var newGrid = grid.Clone();

            var repeat = true;
            var counter = 0;
            while (repeat)
            {
                repeat = false;
                counter++;
                for (int y = 0; y < grid.Count; y++)
                {
                    for (int x = 0; x < grid[y].Count; x++)
                    {
                        var list = new List<char>();
                        list.Add(checkNeighbors(y - 1, x - 1, grid, -1, -1));
                        list.Add(checkNeighbors(y - 1, x, grid, -1, 0));
                        list.Add(checkNeighbors(y - 1, x + 1, grid, -1, 1));
                        list.Add(checkNeighbors(y, x - 1, grid, 0, -1));
                        list.Add(checkNeighbors(y, x + 1, grid, 0, 1));
                        list.Add(checkNeighbors(y + 1, x - 1, grid, 1, -1));
                        list.Add(checkNeighbors(y + 1, x, grid, 1, 0));
                        list.Add(checkNeighbors(y + 1, x + 1, grid, 1, 1));
                        if (grid[y][x] == 'L')
                        {
                            if (list.Count(x => x == '#') == 0)
                            {
                                newGrid[y][x] = '#';
                                repeat = true;
                            }
                        }
                        else if (grid[y][x] == '#')
                        {
                            if (list.Count(x => x == '#') >= makeEmpty)
                            {
                                newGrid[y][x] = 'L';
                                repeat = true;
                            }
                        }

                        Console.Write(newGrid[y][x]);
                    }

                    Console.WriteLine();
                }

                grid = newGrid.Clone();
                Console.WriteLine();
            }

            return CountItemsInGrid(grid ,'#');
        }

        private static int CountItemsInGrid(List<List<char>> grid, char c)
        {
            var counter = 0;
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    if (grid[y][x] == c)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }


        private static char CheckNeighbors(int y, int x, List<List<char>> grid, int incX = 0, int incY = 0)
        {
            try
            {
                return grid[y][x];
            }
            catch (Exception e)
            {
                return 'Q';
            }
        }

        private static char CheckNeighborsFirstInSight(int y, int x, List<List<char>> grid, int incY = 0, int incX = 0)
        {

            try
            {
                if (incY == 0 && incX == -1 && y == 0 && x == 1)
                {
                    var xd = "xd";
                }
                while (true)
                {
                    if (grid[y][x] == 'L' || grid[y][x] == '#')
                    {
                        return grid[y][x];
                    }

                    y += incY;
                    x += incX;
                }
            }
            catch (Exception e)
            {
                return 'Q';
            }
        }
    }
}