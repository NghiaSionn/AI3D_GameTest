using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    //constructor
    public void Seletor()
    {

    }

    public void Seletor(string name)
    {
        this.name = name;
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
}
