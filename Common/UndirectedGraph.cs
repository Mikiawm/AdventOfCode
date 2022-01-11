using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common
{
    public class UndirectedGraph
    {
        private readonly Dictionary<string, Vertex> _vertices;

        public UndirectedGraph(Dictionary<string, Vertex> vertices)
        {
            _vertices = vertices;
        }
        public UndirectedGraph()
        {
            _vertices = new Dictionary<string, Vertex>();
        }

        public void AddVertex(string vertexName)
        {
            _vertices[vertexName] = new Vertex();
        }

        public void AddEdge(string src, string dest, int weight = 1)
        {
            if (!_vertices.ContainsKey(src))
            {
                _vertices[src] = new Vertex();
            }

            if (!_vertices.ContainsKey(dest))
            {
                _vertices[dest] = new Vertex();
            }

            _vertices[src].Add(dest);
            _vertices[dest].Add(src);
        }

        public KeyValuePair<string, Vertex> GetVertex(string vertexName)
        {
            return _vertices.FirstOrDefault(x => x.Key == vertexName);
        }

        // Prints all paths from
        // 's' to 'd'
        public void PrintAllPaths(string s, string d)
        {
            var isVisited = new Dictionary<string,bool>();
            var pathList = new List<string>();

            // add source to path[]
            pathList.Add(s);

            // Call recursive utility
            PrintAllPathsUtil(s, d, isVisited, pathList);
        }

        // A recursive function to print
        // all paths from 'u' to 'd'.
        // isVisited[] keeps track of
        // vertices in current path.
        // localPathList<> stores actual
        // vertices in the current path
        private void PrintAllPathsUtil(string u, string d,
            Dictionary<string, bool> isVisited,
            List<string> localPathList)
        {

            if (u.Equals(d)) {
                Console.WriteLine(string.Join(" ", localPathList));
                // if match found then no need
                // to traverse more till depth
                return;
            }

            // Mark the current node
            isVisited[u] = true;

            // Recur for all the vertices
            // adjacent to current vertex
            foreach(var i in _vertices[u].GetConnections())
            {
                if (!isVisited[i]) {
                    // store current node
                    // in path[]
                    localPathList.Add(i);
                    PrintAllPathsUtil(i, d, isVisited,
                        localPathList);

                    // remove current node
                    // in path[]
                    localPathList.Remove(i);
                }
            }

            // Mark the current node
            isVisited[u] = false;
        }

    }

    public class Vertex
    {
        private readonly List<Edge> _connections;
        private bool isVisited;
        private int _maxVisitCount;

        public Vertex(IEnumerable<string> connections, int maxVisitCount = 1)
        {
            _connections = connections.Select(x => new Edge(x)).ToList();
            _maxVisitCount = maxVisitCount;
        }

        public Vertex()
        {
            _maxVisitCount = 1;
            _connections = new List<Edge>();
        }

        public void Add(string dest)
        {
            _connections.Add(new Edge(dest));
        }

        public List<string> GetConnections()
        {
            return _connections.Select(x => x.GetDest()).ToList();
        }
    }

    public class Edge
    {
        private readonly string _dest;
        private readonly int _weight;

        public Edge(string dest, int weight = 1)
        {
            _dest = dest;
            _weight = weight;
        }

        public string GetDest()
        {
            return _dest;
        }
    }
}

namespace UndirectedGraphsTests
{
    [TestClass]
    public class UndirectedGraphTests
    {
        [TestMethod]
        public void TestMethod1()
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


            CollectionAssert.AreEqual(graph.GetVertex("New York").Value.GetConnections(), new List<string>
            {
                "Bratislava",
                "Warsaw"
            });

            CollectionAssert.AreEqual(graph.GetVertex("Warsaw").Value.GetConnections(), new List<string>
            {
                "Bratislava",
                "New York",
                "Kyiv"
            });
        }
    }
}