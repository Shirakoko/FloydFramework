using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
         // 1. 创建图结构
        var graph = new MyGraph<CityNode, RoadEdge>();

        // 2. 创建城市节点
        var beijing = new CityNode("Beijing");
        var shanghai = new CityNode("Shanghai");
        var guangzhou = new CityNode("Guangzhou");
        var shenzhen = new CityNode("Shenzhen");
        var chengdu = new CityNode("Chengdu");
        var xian = new CityNode("Xian");

        // 3. 添加节点到图中
        graph.AddNode(beijing);
        graph.AddNode(shanghai);
        graph.AddNode(guangzhou);
        graph.AddNode(shenzhen);
        graph.AddNode(chengdu);
        graph.AddNode(xian);
        
        // 4. 添加道路(边)到图中
        graph.AddEdge(beijing, shanghai, new RoadEdge(1200));
        graph.AddEdge(shanghai, beijing, new RoadEdge(1200));
        
        graph.AddEdge(beijing, xian, new RoadEdge(1000));
        graph.AddEdge(xian, beijing, new RoadEdge(1000));
        
        graph.AddEdge(shanghai, guangzhou, new RoadEdge(1400));
        graph.AddEdge(guangzhou, shanghai, new RoadEdge(1400));
        
        graph.AddEdge(guangzhou, shenzhen, new RoadEdge(150));
        graph.AddEdge(shenzhen, guangzhou, new RoadEdge(150));
        
        graph.AddEdge(xian, chengdu, new RoadEdge(700));
        graph.AddEdge(chengdu, xian, new RoadEdge(700));
        
        graph.AddEdge(chengdu, guangzhou, new RoadEdge(1300));
        graph.AddEdge(guangzhou, chengdu, new RoadEdge(1300));

        // 5. 创建Floyd搜索器
        var allNodes = new List<CityNode> { beijing, shanghai, guangzhou, shenzhen, chengdu, xian };
        var floydSearcher = new FloydSearcher<MyGraph<CityNode, RoadEdge>, CityNode>(graph, allNodes);

        // 6. 计算所有节点对的最短路径
        floydSearcher.CalculateAllPairsShortestPaths();
        
        // 7. 检查负权环
        Debug.Log($"图中是否存在负权环: {floydSearcher.HasNegativeCycle()}");
        
        // 8. 查询几个城市之间的最短距离和路径
        QueryAndPrint(floydSearcher, beijing, shenzhen);
        QueryAndPrint(floydSearcher, shanghai, chengdu);
        QueryAndPrint(floydSearcher, xian, guangzhou);
        QueryAndPrint(floydSearcher, beijing, beijing);
    }

    private static void QueryAndPrint(FloydSearcher<MyGraph<CityNode, RoadEdge>, CityNode> searcher, CityNode from, CityNode to)
    {
        float distance = searcher.GetShortestDistance(from, to);
        if (float.IsInfinity(distance))
        {
            Debug.Log($"从 {from} 到 {to} 没有可达路径");
            return;
        }
        
        Debug.Log($"从 {from} 到 {to} 最短距离: {distance} km");
        
        var path = searcher.GetShortestPath(from, to);
        Debug.Log("路径: " + string.Join(" -> ", path));
    }
}
