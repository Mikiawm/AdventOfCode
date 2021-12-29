using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AdventOfCode2020.Day4
{
    public static class Day4
    {
        public static int GetResults(Func<Dictionary<string, string>, bool> validInput)
        {
            var fileReader = new FileReader("Day4/input.txt");

            var input = fileReader.GetFileText();


            var inputObjectStrings = input.Split(new string[] {"\r\n\r\n"},
                StringSplitOptions.RemoveEmptyEntries);
            var counter = 0;

            foreach (var inputObjectString in inputObjectStrings)
            {
                var objectFields = inputObjectString.Split(new string[] {"\r\n", " "},
                    StringSplitOptions.RemoveEmptyEntries);
                var objectDict = new Dictionary<string, string>();
                foreach (var objectField in objectFields)
                {
                    objectDict.Add(objectField.Split(":").First(), objectField.Split(":").Last());
                }

                var b = validInput(objectDict);
                if (b) counter++;
            }


            return counter;
        }

        public static int GetResultsPart1()
        {
            return GetResults(ValidInput);
        }

        private static bool ValidInput(Dictionary<string, string> objectDict)
        {
            var requiredFields = new List<string>()
            {
                "ecl",
                "pid",
                "eyr",
                "hcl",
                "byr",
                "iyr",
                "hgt"
            };
            var b = objectDict.Keys.Intersect(requiredFields).Count() == requiredFields.Count();
            return b;
        }

        public static int GetResultsPart2()
        {
            return GetResults(ValidInputPart2);
        }

        public static bool ValidInputPart2(Dictionary<string, string> objectDict)
        {
            //     byr (Birth Year) - four digits; at least 1920 and at most 2002.
            //     iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            //     eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            //     hgt (Height) - a number followed by either cm or in:
            //     If cm, the number must be at least 150 and at most 193.
            //     If in, the number must be at least 59 and at most 76.
            //     hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            //     ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            //     pid (Passport ID) - a nine-digit number, including leading zeroes.
            //     cid (Country ID) - ignored, missing or not.
            var requiredFields = new List<string>()
            {
                "ecl",
                "pid",
                "eyr",
                "hcl",
                "byr",
                "iyr",
                "hgt"
            };

            foreach (var requiredField in requiredFields)
            {
                try
                {
                    objectDict.TryGetValue(requiredField, out string objectValue);
                    switch (requiredField)
                    {
                        case "byr":
                            if (!(int.Parse(objectValue) >= 1920 && int.Parse(objectValue) <= 2002)) return false;
                            break;
                        case "iyr":
                            if (!(int.Parse(objectValue) >= 2010 && int.Parse(objectValue) <= 2020)) return false;
                            break;
                        case "eyr":
                            if (!(int.Parse(objectValue) >= 2020 && int.Parse(objectValue) <= 2030)) return false;
                            break;
                        case "hgt":
                            var digits = string.Concat(objectValue.Select(x => x).Where(char.IsDigit));
                            var letters =  string.Concat(objectValue.Select(x => x).Where(x => !char.IsDigit(x)));

                            if (letters == "cm")
                            {
                                if (int.Parse(digits) >= 150 && int.Parse(digits) <= 193) break;
                            }

                            if (letters == "in")
                            {
                                if (int.Parse(digits) >= 59 && int.Parse(digits) <= 76) break;
                            }

                            return false;
                        case "hcl":
                            if (objectValue.First() != '#') return false;
                            var stringWithoutFirst = objectValue.Substring(1);
                            var matchedCount = Regex.IsMatch(stringWithoutFirst, @"^[0-9a-f]+$");
                            if (stringWithoutFirst.Count() != 6) return false;
                            if (!matchedCount) return false;
                            break;
                        case "ecl":
                            var eyeColors = new List<string>
                            {
                                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
                            };
                            if (eyeColors.Contains(objectValue)) break;
                            return false;
                        case "pid":
                            var isOk = Regex.IsMatch(objectValue, @"^[0-9a-f]+$");
                            if (objectValue.Count() == 9 && isOk) break;
                            return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
