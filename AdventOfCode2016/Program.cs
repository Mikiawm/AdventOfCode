using System;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var result in Day4.Day4.GetResultPart2())
            {
                Console.WriteLine(result);

                if (result.Contains("north"))
                {
                    Console.WriteLine(result);
                }

            }
        }
    }
}