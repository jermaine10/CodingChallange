using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public class SolutionTest
{
    [Test]
    public void FixedTests()
    {
        Assert.AreEqual(new List<int>(), BinaryTreeSort.TreeByLevels(null));
        Assert.AreEqual(new List<int>() { 1, 2, 3, 4, 5, 6 }, BinaryTreeSort.TreeByLevels(new Node(new Node(null, new Node(null, null, 4), 2), new Node(new Node(null, null, 5), new Node(null, null, 6), 3), 1)));
    }

    [Test]
    public void RandomTests()
    {
        for (int i = 0; i < 100; i++)
        {
            Node node;
            List<int> li;

            makeTree(out node, out li);

            Assert.AreEqual(BinaryTreeSort.TreeByLevels(node), li);
        }
    }

    private void makeTree(out Node node, out List<int> li)
    {
        node = null;
        li = new List<int>();
        Random r = new Random();
        if (r.NextDouble() > 0.8)
            return;
        List<Node> a = new List<Node>() { new Node(null, null, r.Next(1, 1000)) };
        int maxSize = r.Next(7, 127);
        for (int i = 0; i < a.Count; i++)
        {
            if (a.Count > maxSize)
                break;
            if (r.NextDouble() > 0.25)
            {
                Node n = new Node(null, null, r.Next(1, 1000));
                a[i].Left = n;
                a.Add(n);
            }
            if (r.NextDouble() > 0.25)
            {
                Node n = new Node(null, null, r.Next(1, 1000));
                a[i].Right = n;
                a.Add(n);
            }
        }
        node = a[0];
        li = new List<int>();
        foreach (Node n in a)
        {
            li.Add(n.Value);
        }
    }
}