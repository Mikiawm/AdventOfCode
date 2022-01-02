using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day9
{
    public static class Day9
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day9/input.txt");

            var points = fileReader.GetFileLines().Select((items, y) => items.ToCharArray().Select((valueChar, x) => new
            {
                x,
                y,
                value = int.Parse(valueChar.ToString())
            })).SelectMany(x => x).ToList();

            var sum = 0;

            foreach (var point in points)
            {
                try
                {
                    var upPoint = points.FirstOrDefault(x => x.y == point.y + 1 && x.x == point.x);
                    var downPoint = points.FirstOrDefault(x => x.y == point.y - 1 && x.x == point.x);
                    var leftPoint = points.FirstOrDefault(x => x.x == point.x - 1 && x.y == point.y);
                    var rightPoint = points.FirstOrDefault(x => x.x == point.x + 1 && x.y == point.y);

                    var localSum = 0;
                    if (upPoint == null || upPoint.value > point.value)
                    {
                        localSum += 1;
                    }

                    if (downPoint == null || downPoint.value > point.value)
                    {
                        localSum += 1;
                    }

                    if (leftPoint == null || leftPoint.value > point.value)
                    {
                        localSum += 1;
                    }

                    if (rightPoint == null || rightPoint.value > point.value)
                    {
                        localSum += 1;
                    }

                    if (localSum == 4)
                    {
                        sum += point.value + 1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return sum;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day9/input.txt");

            var points = fileReader.GetFileLines().Select((items, y) => items.ToCharArray().Select((valueChar, x) => new BasinPoint
            {
                X = x,
                Y = y,
                Value = int.Parse(valueChar.ToString())
            })).SelectMany(x => x).ToList();

            var basinPoints = new List<BasinPoint>();
            foreach (var point in points)
            {
                try
                {
                    var (upPoint, downPoint, leftPoint, rightPoint) = AdjacentPoints(points, point);

                    var localSum = 0;
                    if (upPoint == null || upPoint.Value > point.Value)
                    {
                        localSum += 1;
                    }

                    if (downPoint == null || downPoint.Value > point.Value)
                    {
                        localSum += 1;
                    }

                    if (leftPoint == null || leftPoint.Value > point.Value)
                    {
                        localSum += 1;
                    }

                    if (rightPoint == null || rightPoint.Value > point.Value)
                    {
                        localSum += 1;
                    }

                    if (localSum == 4)
                    {
                        basinPoints.Add(point);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            //
            List<List<BasinPoint>> itemsMine = new List<List<BasinPoint>>();

            foreach (var basinPoint in basinPoints)
            {
                var (upPoint, downPoint, leftPoint, rightPoint) = AdjacentPoints(points, basinPoint);
                var allItems = new List<BasinPoint>
                {
                    basinPoint
                };
                var items = NewMethod(upPoint, basinPoint, downPoint, leftPoint, rightPoint).Where(x => x != null).ToList();
                allItems.AddRange(items);
                while (true)
                {
                    var itemsTempTemp = new List<BasinPoint>();
                    foreach (var item in items)
                    {
                        (upPoint, downPoint, leftPoint, rightPoint) = AdjacentPoints(points, item);
                        var itemsTemp = NewMethod(upPoint, item, downPoint, leftPoint, rightPoint).Where(x => x != null).ToList();
                        if (itemsTemp.Any())
                        {
                            allItems.AddRange(itemsTemp);
                            itemsTempTemp.AddRange(itemsTemp);
                        }
                    }

                    if (!itemsTempTemp.Any())
                    {
                        break;
                    }

                    items = itemsTempTemp;

                }

                itemsMine.Add(allItems.Where(x => x != null).ToList());

            }

            return itemsMine.Select(x => x.GroupBy(y => y).Count()).OrderByDescending(i => i).Take(3).Aggregate((a, x) => a * x);
        }

        private static IEnumerable<BasinPoint> NewMethod(BasinPoint upPoint, BasinPoint basinPoint, BasinPoint downPoint, BasinPoint leftPoint, BasinPoint rightPoint)
        {
            var relatedBasinPoints = new List<BasinPoint>();
            if (upPoint == null || upPoint.Value > basinPoint.Value && upPoint.Value != 9)
            {
                relatedBasinPoints.Add(upPoint);
            }

            if (downPoint == null || downPoint.Value > basinPoint.Value && downPoint.Value != 9)
            {
                relatedBasinPoints.Add(downPoint);
            }

            if (leftPoint == null || leftPoint.Value > basinPoint.Value && leftPoint.Value != 9)
            {
                relatedBasinPoints.Add(leftPoint);
            }

            if (rightPoint == null || rightPoint.Value > basinPoint.Value && rightPoint.Value != 9)
            {
                relatedBasinPoints.Add(rightPoint);
            }

            return relatedBasinPoints;
        }

        private static (BasinPoint upPoint, BasinPoint downPoint, BasinPoint leftPoint, BasinPoint rightPoint) AdjacentPoints(IReadOnlyCollection<BasinPoint> points, BasinPoint point)
        {
            var upPoint = points.FirstOrDefault(x => x.Y == point.Y + 1 && x.X == point.X);
            var downPoint = points.FirstOrDefault(x => x.Y == point.Y - 1 && x.X == point.X);
            var leftPoint = points.FirstOrDefault(x => x.X == point.X - 1 && x.Y == point.Y);
            var rightPoint = points.FirstOrDefault(x => x.X == point.X + 1 && x.Y == point.Y);
            return (upPoint, downPoint, leftPoint, rightPoint);
        }
    }

    public class BasinPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
    }
}