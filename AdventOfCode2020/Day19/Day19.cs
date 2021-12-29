using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace AdventOfCode2020.Day19
{
    public static class Day19
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day19/input.txt");

            var fileParts = fileReader.GetFileText().Split(new string[] {Environment.NewLine + Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);

            var rulesDictionary = fileParts[0]
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(':'))
                .ToList()
                .ToDictionary(x => x.First(), y => y.Last().Trim().Trim('"'));

            var messages = fileParts[1]
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);


            var rulesTrees = new List<string>()
            {
                rulesDictionary["0"]
            };
            while (true)
            {
                if (!rulesTrees.Any(x => x.Any(char.IsDigit)))
                {
                    break;
                }

                var newRulesTree = BuildNewRulesTree(rulesTrees, rulesDictionary);
                rulesTrees = newRulesTree;
            }

            rulesTrees = rulesTrees.Select(x => x.Replace(" ", string.Empty)).ToList();
            var xd = messages.Count(x => rulesTrees.Contains(x));

            return xd;
        }

        //8: 42 | 42 8
        //11: 42 31 | 42 11 31
        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day19/input.txt");

            var fileParts = fileReader.GetFileText().Split(new string[] {Environment.NewLine + Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);

            var rulesDictionary = fileParts[0]
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(':'))
                .ToList()
                .ToDictionary(x => x.First(), y => y.Last().Trim().Trim('"'));

            rulesDictionary["8"] = "42 | 42 8";
            rulesDictionary["11"] = "42 31 | 42 11 31";

            var messages = fileParts[1]
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);


            var rulesTrees = new List<string>()
            {
                rulesDictionary["0"]
            };
            while (true)
            {
                if (!rulesTrees.Any(x => x.Any(char.IsDigit)))
                {
                    break;
                }

                var newRulesTree = BuildNewRulesTree(rulesTrees, rulesDictionary);
                rulesTrees = newRulesTree;
            }

            rulesTrees = rulesTrees.Select(x => x.Replace(" ", string.Empty)).ToList();
            var xd = messages.Count(x => rulesTrees.Contains(x));

            return xd;
        }


        private static List<string> BuildNewRulesTree(List<string> rulesTree,
            Dictionary<string, string> rulesDictionary)
        {
            var listToBuild = new List<List<List<string>>>();
            foreach (var ruleTree in rulesTree)
            {
                var newTree = new List<List<string>>();
                foreach (var rule in ruleTree.Split(' '))
                {
                    if (!char.IsLetter(rule[0]))
                    {
                        var rules = GetPipedRules(rulesDictionary[rule]);

                        newTree.Add(rules.ToList());
                    }
                    else
                    {
                        newTree.Add(new List<string> {rule});
                    }
                }

                listToBuild.Add(newTree);
            }

            var returnList = new List<string>();

            foreach (var listOfNewTree in listToBuild)
            {
                foreach (var newRulesBuild in BuildNewRulesTreeFromList(listOfNewTree).ToList())
                {
                    returnList.Add(string.Join("", newRulesBuild));
                }
            }

            return returnList;
        }

        private static IEnumerable<string> BuildNewRulesTreeFromList(List<List<string>> newTrees)
        {
            var returnStrings = new List<string>
            {
                ""
            };
            foreach (var newTree in newTrees)
            {
                var xd = returnStrings.Count;
                var newReturnStrings = new List<string>();
                for (int i = 0; i < xd; i++)
                {
                    foreach (var treeElement in newTree)
                    {
                        var addElement = returnStrings[i] + " " + treeElement;
                        newReturnStrings.Add(addElement.Trim());
                    }
                }

                returnStrings = newReturnStrings;
            }

            return returnStrings;
        }

        private static IEnumerable<string> GetPipedRules(string rules)
        {
            return rules.Split('|').Select(x => x.Trim());
        }
    }
}