using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day12
{
    public static class Day12
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day12/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var graph = new UndirectedGraph();

            foreach (var fileLine in fileLines)
            {
                var src = fileLine.Split("-").First();
                var dst = fileLine.Split("-").Last();
                graph.AddEdge(src, dst);
            }

            Console.WriteLine(graph.ToString());
            var paths = graph.ReturnAllPaths("start", "end");

            return paths.Count;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day12/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();
            var graph = new UndirectedGraph();

            foreach (var fileLine in fileLines)
            {
                var src = fileLine.Split("-").First();
                var dst = fileLine.Split("-").Last();
                graph.AddEdge(src, dst);
            }

            var paths = graph.ReturnAllPathsPart2("start", "end", new List<string>());

            return paths.Count;
        }
    }
}