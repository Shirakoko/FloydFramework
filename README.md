博客地址：[游戏算法-Floyd搜索算法知识梳理和通用框架 | 白雪团子](https://www.shirakoko.xyz/article/floyd)

- /FloydFramework：通用的Floyd搜索算法框架
  - FloydNode.cs：节点泛型接口，继承该接口的节点可作为Floyd算法中的节点
  - FloydSearcher.cs：Floyd算法搜索器，使用时需传入节点类类型和地图类类型泛型
- /AstarUsage：一个使用示例
  - MyGraph.cs：一个自己实现的图数据结构
  - CityNode.cs：表示城市节点
  - RoadEdge.cs：表示城市之间的道路
  - Test.cs：测试脚本
