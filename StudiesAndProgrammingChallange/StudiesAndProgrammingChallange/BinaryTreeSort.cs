using System;
using System.Linq;
using System.Collections.Generic;
class BinaryTreeSort
{
    public static List<int> TreeByLevels(Node node)
    {
        List<int> result = new List<int>();
        List<Node> test = new List<Node>();

        if (node != null)
            test.Add(node);
        int i = 0;
        while (i < test.Count)
        {
            if (test[i].Left != null) test.Add(test[i].Left);
            if (test[i].Right != null) test.Add(test[i].Right);
            i++;
        }
        return test.Select(x => x.Value).ToList();
    }
}
public class Node
{
    public Node Left;
    public Node Right;
    public int Value;

    public Node(Node l, Node r, int v)
    {
        Left = l;
        Right = r;
        Value = v;
    }
}
