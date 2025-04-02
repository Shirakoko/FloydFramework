using System.Collections.Generic;

public class FloydSearcher<T_Map, T_Node> where T_Node : IFloydNode<T_Node>
{
    // 搜索空间（地图）
    private readonly T_Map nodeMap;

    // 距离矩阵
    private float[,] distanceMatrix;
    // 路径矩阵
    private int[,] nextMatrix;
    // 节点列表
    private List<T_Node> nodeList;
    // 是否已计算
    private bool isCalculated = false;

    public FloydSearcher(T_Map map, List<T_Node> nodes)
    {
        nodeMap = map;
        nodeList = nodes;
        InitializeDistanceMatrix();
    }

    /// <summary>
    /// 初始化距离矩阵和节点的Parent指针
    /// </summary>
    private void InitializeDistanceMatrix()
    {
        int nodeCount = nodeList.Count;
        distanceMatrix = new float[nodeCount, nodeCount];
        nextMatrix = new int[nodeCount, nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                if (i == j) {
                    distanceMatrix[i, j] = 0; // 自己到自己的距离是0
                    nextMatrix[i, j] = i; // 路径矩阵的对角线是自身
                } else {
                    float distance = nodeList[i].GetDistance(nodeList[j], nodeMap);
                    distanceMatrix[i, j] = distance;
                    nextMatrix[i, j] = (distance < float.PositiveInfinity) ? j : -1; // 直接相连则i到j的下一步是j，否则是-1
                }
            }
        }
    }

    /// <summary>
    /// 计算所有节点对的最短路径
    /// </summary>
    public void CalculateAllPairsShortestPaths()
    {
        if (isCalculated) return;

        int nodeCount = nodeList.Count;

        // Floyd算法核心：动态规划更新
        for (int k = 0; k < nodeCount; k++)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    // 检查通过k节点是否更短
                    float newDistance = distanceMatrix[i, k] + distanceMatrix[k, j];
                    if (newDistance < distanceMatrix[i, j])
                    {
                        // 更新最小距离
                        distanceMatrix[i, j] = newDistance;
                        // 更新路径
                        nextMatrix[i, j] = nextMatrix[i, k]; 
                    }
                }
            }
        }

        isCalculated = true;
    }

    /// <summary>
    /// 获取两节点间的最短距离
    /// </summary>
    /// <param name="from">起始节点</param>
    /// <param name="to">目标节点</param>
    /// <returns>最短距离，若无路径返回float.PositiveInfinity</returns>
    public float GetShortestDistance(T_Node from, T_Node to)
    {
        if (!isCalculated) CalculateAllPairsShortestPaths();

        int i = nodeList.IndexOf(from);
        int j = nodeList.IndexOf(to);

        if (i == -1 || j == -1)
            return float.PositiveInfinity;

        return distanceMatrix[i, j];
    }

     /// <summary>
    /// 获取两节点间的最短路径
    /// </summary>
    /// <param name="from">起始节点</param>
    /// <param name="to">目标节点</param>
    /// <returns>路径节点列表（按顺序从起点到终点），若无路径返回空列表</returns>
    public List<T_Node> GetShortestPath(T_Node from, T_Node to)
    {
        if (!isCalculated) CalculateAllPairsShortestPaths();

        int u = nodeList.IndexOf(from);
        int v = nodeList.IndexOf(to);

        if (u == -1 || v == -1 || float.IsInfinity(distanceMatrix[u, v]))
            return new List<T_Node>();

        var path = new List<T_Node>{nodeList[u]};

        while (u != v)
        {
            u = nextMatrix[u, v];
            path.Add(nodeList[u]);
        }

        return path;
    }

    /// <summary>
    /// 检查图中是否存在负权环
    /// </summary>
    /// <returns>存在负权环返回true</returns>
    public bool HasNegativeCycle()
    {
        if (!isCalculated) CalculateAllPairsShortestPaths();

        for (int i = 0; i < nodeList.Count; i++)
        {
            if (distanceMatrix[i, i] < 0)
                return true;
        }
        return false;
    }
}
