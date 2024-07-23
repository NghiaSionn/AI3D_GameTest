using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    public Inverter(string n)
    {
        name = n;
    }

    public override Status Process()
    {
        var childStatus = children[0].Process();
        if (childStatus == Status.Running) return Status.Running;
        if (childStatus == Status.Failure) return Status.Success;
        else return Status.Failure;
    }
}
