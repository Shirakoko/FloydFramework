using System.Collections.Generic;

public interface IFloydNode<T> where T : IFloydNode<T>
{
    /// <summary>
    /// 获取到另一个节点的实际距离（从地图数据中获取）
    /// </summary>
    /// <param name="otherNode">目标节点</param>
    /// <param name="nodeMap">包含节点连接信息的图结构</param>
    /// <returns>两节点间的实际距离</returns>
    float GetDistance(T otherNode, object nodeMap);

    /// <summary>
    /// 获取所有直接相连的邻居节点
    /// </summary>
    /// <param name="nodeMap">寻路所在的图结构</param>
    /// <returns>邻居节点列表</returns>
    List<T> GetSuccessors(object nodeMap);

    /// <summary>
    /// 判断节点是否相等（用于字典和集合操作）
    /// </summary>
    bool Equals(T other);
}