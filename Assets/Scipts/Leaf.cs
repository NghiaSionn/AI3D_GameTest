using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    //delegate xử lý node
    public delegate Status Tick();

    //hàm xử lý node
    public Tick ProcessMethod;

   //constructor
    public Leaf() 
    {

    }
    public Leaf(string name,Tick process, int order)
    {
        this.name = name;
        ProcessMethod = process;
        sortOrder = order;
    }

    // hàm chạy node, trả về trạng thái của node
    public override Status Process()
    {
        if(ProcessMethod != null)
        {
            return ProcessMethod();
        }

        return Status.Failure; 
    }

}
