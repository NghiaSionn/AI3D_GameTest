using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    //conructor 
    public Sequence()
    {

    }

    public Sequence(string name)
    {
        this.name = name;
    }

    //hàm chạy node , trả về trạnh thái của node
    public override Status Process()
    {
        var childStatus = children[currentChild].Process();
        if(childStatus == Status.Running) return Status.Running;
        if(childStatus == Status.Failure) return Status.Failure;
        currentChild++;
        if(currentChild >= children.Count)
        {
            currentChild = 0;
            return Status.Success;
        }
        return Status.Running;
    }
}
