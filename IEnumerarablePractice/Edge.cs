using System;
using System.Collections.Generic;
using System.Linq;

public class Edge
{
    public string Source { get; set; }
    public string Destination { get; set; }

    public static List<Edge> GetGraphEdges()
    {
        return new List<Edge>()
        {
            new Edge{ Source="A", Destination="B"},
            new Edge{ Source="A", Destination="A"},
            new Edge{ Source="A", Destination="C"},
            new Edge{ Source="A", Destination="D"},
            new Edge{ Source="A", Destination="E"},

            new Edge{ Source="B", Destination="C"},
            new Edge{ Source="B", Destination="D"},

            new Edge{ Source="C", Destination="D"},
            new Edge{ Source="C", Destination="E"},

            new Edge{ Source="D", Destination="E"},
            new Edge{ Source="E", Destination="F"}
        };
    }
}