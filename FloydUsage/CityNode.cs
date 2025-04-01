using System.Collections.Generic;
using System.Linq;

public class CityNode : IFloydNode<CityNode>
{
    public string Name { get; }

    public CityNode(string name)
    {
        Name = name;
    }

    public float GetDistance(CityNode otherNode, object nodeMap)
    {
        if (nodeMap is MyGraph<CityNode, RoadEdge> graph)
        {
            var edges = graph.FindEdge(this, otherNode);
            if (edges != null && edges.Count > 0)
            {
                // 如果有多个边，取最短的一条
                return edges.Min(e => e.Distance);
            }
        }
        return float.PositiveInfinity; // 没有直接连接
    }

    public List<CityNode> GetSuccessors(object nodeMap)
    {
        if (nodeMap is MyGraph<CityNode, RoadEdge> graph)
        {
            return graph.GetNeighbor(this) ?? new List<CityNode>();
        }
        return new List<CityNode>();
    }

    public bool Equals(CityNode other)
    {
        return other != null && Name == other.Name;
    }

    public override string ToString() => Name;
}