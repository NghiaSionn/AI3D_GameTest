using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class BehaviorTree : Node
{
    // contructor
    public BehaviorTree() 
    {
        this.name = "Root";
    }

    public BehaviorTree(string name)
    {
        this.name = name;
    }
    
    //level của node
    struct NodeLevel
    {
        public Node node;
        public int level;
    }

    public override Status Process()
    {
        if(children.Count == 0)
        {
            return Status.Success;
        }

        return children[currentChild].Process();
    }

    //in cây ra màn hình
    public void PrintTree()
    {
        var tree = "";
        var stack = new Stack<NodeLevel>();
        Node current = this;
        stack.Push(new NodeLevel {node = current, level = 0});
        while (stack.Count != 0) 
        {
            var nextNode = stack.Pop();
            tree += new string('-', nextNode.level) + nextNode.node.name + "\n";
            foreach (var child in nextNode.node.children)
            {
                stack.Push(new NodeLevel
                {
                    node = child,
                    level = nextNode.level + 1
                });
            }
        }
        Debug.Log(tree);
    }
}
