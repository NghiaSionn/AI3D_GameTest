using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSelector : Node
{
    //constructor
    public PSelector()
    {

    }

    public PSelector(string name)
    {
        this.name = name;
    }

    void OrderNodes()
    {
        Node[] arr = children.ToArray(); 
       // Sort(arr, 0 , children.Count - 1);
        children = new List<Node>(arr);
    }

    //hàm chạy node , trả về trạng thái của node
    public override Status Process()
    {
        var childStatus = children[currentChild].Process();
        if (childStatus == Status.Running) return Status.Running;
        if (childStatus == Status.Success)
        {
            currentChild = 0;
            return Status.Success;
        }

        currentChild++;

        if (currentChild >= children.Count)
        {
            currentChild = 0;
            return Status.Failure;
        }
        return Status.Running;
    }
  
    //void Sort(Node[] arr, int left, int right)
    //{
    //    if (left < right)
    //    {
    //        int pivot = Partition(arr, left, right);


    //    }

    //}

    //int Partition(Node[] arr, int left, int right)
    //{

    //}
    
}
