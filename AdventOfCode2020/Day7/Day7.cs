using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day7
{
    public static class Day7
    {
        private static List<string> numbers = new List<string>();
        private static int result = 0;
        public static int GetResultsPart1()
        {
            var fileReader = new FileReader("Day7/input.txt");

            var getFileLines = fileReader.GetFileLines();

            var possibleBags = new List<string>();

            var searchBags = new List<string>() {"shiny gold"};



            SearchForBags(getFileLines, searchBags, possibleBags);

            return possibleBags.Distinct().Count();
        }

        private static void SearchForBags(IEnumerable<string> getFileLines, List<string> searchBags, List<string> possibleBags)
        {
            var searchBagsTemp = new List<string>();
            foreach (var searchBag in searchBags)
            {
                foreach (var fileLine in getFileLines)
                {
                    var containItems = fileLine.Split("contain").Last();

                    var bags = containItems.Split(',');

                    var bagsContain = bags.Any(x => x.Contains(searchBag));


                    var bagName = fileLine.Split("contain").First().Split();

                    if (bagsContain)
                    {
                        searchBagsTemp.Add(bagName[0] + " " + bagName[1]);
                        possibleBags.Add(bagName[0] + " " + bagName[1]);
                    }
                }
            }

            if (searchBagsTemp.Any())
            {
                SearchForBags(getFileLines, searchBagsTemp, possibleBags);
            }

        }

        public static int GetResultsPart2()
        {
            var fileReader = new FileReader("Day7/input.txt");

            var getFileLines = fileReader.GetFileLines();

            List<string> searchBags = new List<string>();

            foreach (var fileLine in getFileLines)
            {
                var bagName = fileLine.Split("contain").First();

                if (bagName.Contains("shiny gold"))
                {
                    var containItems = fileLine.Split("contain").Last();

                    var bags = containItems.Split(',');

                    searchBags.AddRange(bags);
                }
            }



            int numbers = SearchForBags2(getFileLines, searchBags);

            return numbers;
        }

        private static int SearchForBags2(IEnumerable<string> getFileLines, List<string> searchBags, int start = 0)
        {
            var currentResult = 0;
            foreach (var searchBag in searchBags)
            {
                var bagValues = searchBag.Trim().Split(' ');

                if (searchBag == " no other bags.") return 0;
                var bagCount = int.Parse(bagValues[0]);
                var bagName = bagValues[1] + ' ' + bagValues[2];
                var newBagList = new List<string>();

                foreach (var fileLine in getFileLines)
                {
                    var bagNameTemp = fileLine.Split("contain").First();

                    if (bagNameTemp.Contains(bagName))
                    {
                        var containItems = fileLine.Split("contain").Last();

                        newBagList.AddRange(containItems.Split(','));
                    }
                }
                currentResult +=  bagCount + (bagCount * SearchForBags2(getFileLines, newBagList));

            }

            return currentResult;
        }
    }
}