using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day8
{
    public static class Day8
    {
        public static int GetResultsPart1()
        {
            var fileReader = new FileReader("Day8/input.txt");

            var getFileLines = fileReader.GetFileLines();

            var instructions = getFileLines.Select(x => new KeyValuePair<string, int>(x, 0)).ToList();
            return Sum(instructions);
        }

        private static int Sum(List<KeyValuePair<string, int>> instructions)
        {
            var accumulation = 0;
            for (int i = 0; i < instructions.Count(); i++)
            {
                var instruction = instructions[i].Key;
                if (instructions[i].Value == 1)
                {
                    return accumulation;
                }

                if (instruction.StartsWith("acc"))
                {
                    var accValue = instruction.Split(" ").Last();

                    if (accValue.StartsWith("+")) accumulation += int.Parse(accValue.Substring(1));
                    if (accValue.StartsWith("-")) accumulation -= int.Parse(accValue.Substring(1));
                    instructions[i] = new KeyValuePair<string, int>(instruction, 1);
                }

                if (instruction.StartsWith("jmp"))
                {
                    var accValue = instruction.Split(" ").Last();
                    instructions[i] = new KeyValuePair<string, int>(instruction, 1);
                    if (accValue.StartsWith("+")) i += int.Parse(accValue.Substring(1)) - 1;
                    if (accValue.StartsWith("-")) i -= int.Parse(accValue.Substring(1)) + 1;
                }
            }

            return accumulation;
        }

        public static int GetResultsPart2()
        {
            var fileReader = new FileReader("Day8/input.txt");

            var getFileLines = fileReader.GetFileLines();

            int accumulation = 0;
            var instructions = getFileLines.Select(x => new KeyValuePair<string, int>(x, 0)).ToList();
            var indexToChange = 0;
            while (!Accumulation(instructions, ref accumulation, ref indexToChange))
            {
            }


            return accumulation;
        }

        private static bool Accumulation(List<KeyValuePair<string, int>> constInstructions, ref int accumulation,
            ref int indexToChange)
        {
            accumulation = 0;
            var instructions = new List<KeyValuePair<string, int>>();
            instructions.AddRange(constInstructions);
            for (int i = 0; i < instructions.Count(); i++)
            {
                if (instructions[i].Value == 1)
                {
                    indexToChange += 1;
                    return false;
                }

                // nop +0  | 1
                // acc +1  | 2
                // jmp +4  | 3
                // acc +3  |
                // jmp -3  |
                // acc -99 |
                // acc +1  | 4
                // nop -4  | 5
                // acc +6  | 6
                var instruction = instructions[i].Key;

                if (i == indexToChange)
                {
                    if (instruction.StartsWith("jmp"))
                    {
                        instruction = instruction.Replace("jmp", "nop");
                    }
                    else if (instruction.StartsWith("nop"))
                    {
                        instruction = instruction.Replace("nop", "jmp");
                    }
                }


                if (instruction.StartsWith("acc"))
                {
                    var accValue = instruction.Split(" ").Last();

                    if (accValue.StartsWith("+")) accumulation += int.Parse(accValue.Substring(1));
                    if (accValue.StartsWith("-")) accumulation -= int.Parse(accValue.Substring(1));
                    instructions[i] = new KeyValuePair<string, int>(instruction, 1);
                }

                if (instruction.StartsWith("jmp"))
                {
                    var accValue = instruction.Split(" ").Last();
                    instructions[i] = new KeyValuePair<string, int>(instruction, 1);
                    if (accValue.StartsWith("+")) i += int.Parse(accValue.Substring(1)) - 1;
                    if (accValue.StartsWith("-")) i -= int.Parse(accValue.Substring(1)) + 1;
                }
            }

            return true;
        }
    }
}