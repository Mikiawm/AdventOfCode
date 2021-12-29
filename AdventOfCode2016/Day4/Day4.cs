using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using static System.Char;
using Array = System.Array;

namespace AdventOfCode2016.Day4
{
    public static class Day4
    {
        public static IEnumerable<string> GetResultPart2()
        {
            var fileReader = new FileReader("Day4/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();




            foreach (var fileLine in fileLines)
            {
                var roomData = fileLine.Split('-');
                var last = roomData.Last();
                int mainCounter = 0;
                var sectorId = new string(roomData.Last().ToCharArray().Where(IsDigit).ToArray());
                var swiftLetter = int.Parse(sectorId) % 26;
                var checksum = new string(roomData.Last().ToCharArray().Where(IsLetter).ToArray());

                roomData = roomData.Append(checksum).ToArray();

                var encryptedNames = string.Join(String.Empty, roomData.Take(roomData.Length - 1));
                var letterDictionary = new Dictionary<char, int>();
                var shiftedText = ShiftedText(roomData, alphabet, swiftLetter);


                yield return shiftedText;
            }
        }

        public static char cipher(char ch, int key) {
            if (!IsLetter(ch)) {

                return ch;
            }

            char d = IsUpper(ch) ? 'A' : 'a';
            return (char)((ch + key - d) % 26 + d);


        }
        private static string ShiftedText(string[] roomData, char[] alphabet, int swiftLetter)
        {
            var shiftedText = string.Empty;

            foreach (var room in roomData)
            {
                foreach (var character in room)
                {
                    var index = Array.IndexOf(alphabet, character);

                    if (index < 0)
                    {
                        // This character isn't in the array, so don't change it
                        shiftedText += character;
                    }
                    else
                    {


                        shiftedText += cipher(character, swiftLetter);
                    }
                }

                shiftedText += " ";
            }

            return shiftedText;
        }


        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day4/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var mainCounter = 0;

            foreach (var fileLine in fileLines)
            {
                var roomData = fileLine.Split('-');

                var sectorId = new string(roomData.Last().ToCharArray().Where(IsDigit).ToArray());
                var checksum = new string(roomData.Last().ToCharArray().Where(IsLetter).ToArray());

                var encryptedNames = string.Join(String.Empty, roomData.Take(roomData.Length - 1));
                var letterDictionary = new Dictionary<char, int>();
                foreach (var letter in encryptedNames.ToCharArray())
                {
                    if (letterDictionary.ContainsKey(letter))
                    {
                        letterDictionary[letter]++;
                    }
                    else
                    {
                        letterDictionary[letter] = 1;
                    }
                }

                var groupedValue = letterDictionary.OrderByDescending(x => x.Value).GroupBy(x => x.Value);
                var checksumAsCharArray = checksum.ToCharArray().ToList();


                if (ResultPart1(groupedValue, checksumAsCharArray))
                {
                    mainCounter += int.Parse(sectorId);
                }
            }

            return mainCounter;
        }

        private static bool ResultPart1(IEnumerable<IGrouping<int, KeyValuePair<char, int>>> groupedValue,
            List<char> checksumAsCharArray)
        {
            foreach (var group in groupedValue)
            {
                var cc = @group.Select(x => x.Key);

                int counter = 0;
                foreach (var ccitem in cc)
                {
                    if (checksumAsCharArray.Contains(ccitem))
                    {
                        counter++;
                        checksumAsCharArray.Remove(ccitem);
                    }
                }

                if (counter != cc.Count() && checksumAsCharArray.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}