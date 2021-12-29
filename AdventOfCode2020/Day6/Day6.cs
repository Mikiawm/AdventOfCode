using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day6
{
    public static class Day6
    {
        public static int GetResultsPart1()
        {
            var fileReader = new FileReader("Day6/input.txt");

            var input = fileReader.GetFileText();

            var inputObjectStrings = input.Split(new[] {"\r\n\r\n"},
                StringSplitOptions.RemoveEmptyEntries);

            var xd = inputObjectStrings.ToList().Select(x => string.Join("", x.Split(Environment.NewLine)));
            int counter = 0;
            foreach (var group in xd)
            {
                counter += group.Distinct().Count();
            }


            return counter;
        }

        public static int GetResultsPart2()
        {
            var fileReader = new FileReader("Day6/input.txt");

            var input = fileReader.GetFileText();

            var inputObjectStrings = input.Split(new string[] {"\r\n\r\n"},
                StringSplitOptions.RemoveEmptyEntries);

            var groups = inputObjectStrings.ToList().Select(x => x.Split(Environment.NewLine)).ToList();
            int counter = 0;

            groups.ForEach(group =>
            {
                var questions = new Dictionary<char, int>();

                foreach (var form in @group)
                {
                    var answers = form.ToCharArray();
                    foreach (var answer in answers)
                    {
                        if (questions.ContainsKey(answer))
                        {
                            questions[answer]++;
                        }
                        else
                        {
                            questions[answer] = 1;
                        }
                    }
                }

                counter += questions.Count(x => x.Value == @group.Count());
            });

            return counter;
        }
    }
}