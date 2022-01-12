using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common
{
    public class UndirectedGraph
    {
        private readonly Dictionary<string, List<string>> _vertices;

        public UndirectedGraph(Dictionary<string, List<string>> vertices)
        {
            _vertices = vertices;
        }

        public UndirectedGraph()
        {
            _vertices = new Dictionary<string, List<string>>();
        }

        public void AddVertex(string vertexName)
        {
            _vertices[vertexName] = new List<string>();
        }

        public void AddEdge(string src, string dest, int weight = 1)
        {
            if (!_vertices.ContainsKey(src))
            {
                _vertices[src] = new List<string>();
            }

            if (!_vertices.ContainsKey(dest))
            {
                _vertices[dest] = new List<string>();
            }

            _vertices[src].Add(dest);
            _vertices[dest].Add(src);
        }

        public KeyValuePair<string, List<string>> GetVertex(string vertexName)
        {
            return _vertices.FirstOrDefault(x => x.Key == vertexName);
        }

        // Prints all paths from
        // 's' to 'd'
        public List<List<string>> ReturnAllPaths(string start, string end, List<string> path = null)
        {
            path ??= new List<string>();
            path.Add(start);


            if (start == end)
            {
                return new List<List<string>>
                {
                    path
                };
            }

            if (!_vertices.ContainsKey(start))
            {
                return new List<List<string>>();
            }

            var paths = new List<List<string>>();

            var currentVertices = _vertices[start];
            foreach (var vertex in currentVertices)
            {
                if (!path.Contains(vertex) || vertex.ToUpper() == vertex)
                {
                    var pathTemp = new List<string>(path);
                    var newPaths = ReturnAllPaths(vertex, end, pathTemp);
                    foreach (var newPath in newPaths)
                    {
                        paths.Add(newPath);
                    }
                }
            }

            return paths;
        }

        public List<List<string>> ReturnAllPathsPart2(string start, string end, List<string> path = null)
        {
            path ??= new List<string>();
            path.Add(start);

            if (start == end)
            {
                return new List<List<string>>
                {
                    path
                };
            }

            if (!_vertices.ContainsKey(start))
            {
                return new List<List<string>>();
            }

            var paths = new List<List<string>>();

            var currentVertices = _vertices[start];
            foreach (var vertex in currentVertices)
            {
                if (!path.Contains(vertex) || vertex.ToUpper() == vertex || CheckVertex(path, vertex))
                {
                    var pathTemp = new List<string>(path);
                    var newPaths = ReturnAllPathsPart2(vertex, end, pathTemp);
                    foreach (var newPath in newPaths)
                    {
                        paths.Add(newPath);
                    }
                }
            }

            return paths;
        }

        public static bool CheckVertex(List<string> path, string vertex)
        {
            var newPath = path.Append(vertex);
            var groupBy = newPath
                .Where(x => x.ToLower() == x)
                .GroupBy(x => x)
                .ToList();

            var notTooMuch = !groupBy.Any(x => x.Count() > 2);
            var countOptions = groupBy.Count(g => g.Count() == 2) <= 1;
            return vertex.ToLower() != vertex || vertex != "start" && notTooMuch && countOptions;
        }

        public override string ToString()
        {
            var returnString = "";
            foreach (var (key, value) in _vertices)
            {
                returnString += $"{key}: {string.Join(',', value)}";
                returnString += Environment.NewLine;
            }

            return returnString;
        }

        public IEnumerable<string> GetVerticesNames()
        {
            return new List<string>(_vertices.Keys);
        }
    }
}

namespace UndirectedGraphsTests
{
    [TestClass]
    public class UndirectedGraphTests
    {
        [TestMethod]
        public void TestBuildGraph()
        {
            var graph = new UndirectedGraph();
            graph.AddVertex("New York");
            graph.AddVertex("Bratislava");
            graph.AddVertex("Kyiv");
            graph.AddVertex("Warsaw");
            graph.AddVertex("Atlanta");

            graph.AddEdge("New York", "Bratislava");
            graph.AddEdge("Bratislava", "Warsaw");
            graph.AddEdge("Warsaw", "New York");
            graph.AddEdge("Warsaw", "Kyiv");
            graph.AddEdge("Kyiv", "Bratislava");
            graph.AddEdge("Atlanta", "Kyiv");


            CollectionAssert.AreEqual(graph.GetVertex("New York").Value, new List<string>
            {
                "Bratislava",
                "Warsaw"
            });

            CollectionAssert.AreEqual(graph.GetVertex("Warsaw").Value, new List<string>
            {
                "Bratislava",
                "New York",
                "Kyiv"
            });
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(false, UndirectedGraph.CheckVertex(new List<string> {"aa", "aa", "ab", "bb", "bc"}, "aa"));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(true, UndirectedGraph.CheckVertex(new List<string> {"aa", "aa", "ab", "bb", "bc"}, "BB"));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(true, UndirectedGraph.CheckVertex(new List<string> {"aa", "ab", "bb", "bc"}, "aa"));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(true, UndirectedGraph.CheckVertex(new List<string> {"aa", "aa", "ab", "bb", "bc"}, "cc"));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Assert.AreEqual(false, UndirectedGraph.CheckVertex(new List<string> {"aa", "aa", "ab", "bb", "bc"}, "start"));
        }
    }
}