using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day12
{
    public static class Day12
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day12/input.txt");

            var textLines = fileReader.GetFileLines();
            var directionsEnumerable = new List<char> {'E', 'S', 'W', 'N'};
            var facing = 'E';
            var directionsValues = new Dictionary<char, int>
            {
                {'N', 0},
                {'S', 0},
                {'W', 0},
                {'E', 0}
            };

            foreach (var textLine in textLines)
            {
                var direction = textLine.First();
                var moveValue = int.Parse(textLine.Substring(1));


                if (direction == 'R')
                {
                    var directionChange = moveValue / 90;
                    var currentIndex = directionsEnumerable.IndexOf(facing);
                    var newIndex = (currentIndex + directionChange) % directionsEnumerable.Count;
                    facing = directionsEnumerable[newIndex];
                }
                else if (direction == 'L')
                {
                    try
                    {
                        var directionChange = moveValue / 90;
                        var currentIndex = directionsEnumerable.IndexOf(facing);
                        var newIndex = (currentIndex - directionChange) % directionsEnumerable.Count;

                        if (newIndex < 0)
                        {
                            newIndex = directionsEnumerable.Count() + newIndex;
                        }

                        facing = directionsEnumerable[newIndex];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(textLine);
                        throw;
                    }
                }
                else if (direction == 'F')
                {
                    directionsValues[facing] += moveValue;
                }
                else
                {
                    directionsValues[direction] += moveValue;
                }
            }

            var currentPosition = new List<int>
            {
                Math.Abs(directionsValues['E'] - directionsValues['W']),
                Math.Abs(directionsValues['N'] - directionsValues['S'])
            };

            return currentPosition[0] + currentPosition[1];
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day12/input.txt");

            var textLines = fileReader.GetFileLines();
            var directionsEnumerable = new List<char> {'E', 'S', 'W', 'N'};
            var waypoint = new Dictionary<char, int>
            {
                {'N', 1},
                {'S', 0},
                {'W', 0},
                {'E', 10}
            };
            var directionsValues = new Dictionary<char, int>
            {
                {'N', 0},
                {'S', 0},
                {'W', 0},
                {'E', 0}
            };

            foreach (var textLine in textLines)
            {
                var direction = textLine.First();
                var moveValue = int.Parse(textLine.Substring(1));


                if (direction == 'R')
                {
                    var directionChange = moveValue / 90;

                    for (int i = 0; i < directionChange; i++)
                    {
                        var newWaypoint = new Dictionary<char, int>
                        {
                            ['N'] = waypoint['W'],
                            ['E'] = waypoint['N'],
                            ['S'] = waypoint['E'],
                            ['W'] = waypoint['S']

                        };
                        waypoint = new Dictionary<char, int>(newWaypoint);
                    }
                }
                else if (direction == 'L')
                {
                    var directionChange = moveValue / 90;
                    
                    for (int i = 0; i < directionChange; i++)
                    {
                        var newWaypoint = new Dictionary<char, int>
                        {
                            ['N'] = waypoint['E'],
                            ['W'] =  waypoint['N'],
                            ['S'] = waypoint['W'],
                            ['E'] = waypoint['S']
                        };
                        waypoint = new Dictionary<char, int>(newWaypoint);
                    }
                }
                else if (direction == 'F')
                {
                    for (int i = 0; i < moveValue; i++)
                    {
                        foreach (var waypointKey in waypoint.Keys)
                        {
                            directionsValues[waypointKey] += waypoint[waypointKey];
                        }
                    }
                }
                else
                {
                    waypoint[direction] += moveValue;
                }
            }

            var currentPosition = new List<int>
            {
                Math.Abs(directionsValues['E'] - directionsValues['W']),
                Math.Abs(directionsValues['N'] - directionsValues['S'])
            };

            return currentPosition[0] + currentPosition[1];
        }
    }
}