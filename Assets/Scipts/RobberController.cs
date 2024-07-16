using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Robber : MonoBehaviour
{
    private BehaviorTree tree;

    public GameObject diamond;
    public GameObject van;
    public List<GameObject> picture;

    NavMeshAgent agent;

    //trạng thái của nhân vật
    public enum ActionState
    {
        Idle,
        Working
    }

    //khởi động game ở trạng thái idle
    public ActionState actionsState = ActionState.Idle;

    //trạng thái của cây
    Node.Status treeStatus = Node.Status.Running;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tree = new BehaviorTree();

        //tạo các node
        var goToDiamond = new Leaf("Steal Diamond", GoToDiamond);
        var goToVan = new Leaf("Go to Van", GoToVan);
        //var goToPicture = new Leaf("Go to Picture", GoToPicture);

        // tạo node Sequence
        var stealSomething = new Sequence("Steal something");
        stealSomething.AddChild(goToDiamond);
        stealSomething.AddChild(goToVan);

        tree.AddChild(stealSomething);

        //in cây ra màn hình 
        tree.PrintTree();

        ////chạy cây
        //tree.Process();
    }

    //private Node.Status GoToPicture()
    //{
        

    //}

    // hàm xử lý go to dvan
    private Node.Status GoToVan()
    {
        return GoToLocation(van.transform.position);
    }

    // hàm xử lý go to diamond
    private Node.Status GoToDiamond()
    {
        return GoToLocation(diamond.transform.position);
    }

    //Hàm xử lý di chuyển đến vị trí 
    private Node.Status GoToLocation(Vector3 location)
    {
        var distance = Vector3.Distance(transform.position, location);
        if(actionsState == ActionState.Idle) 
        {
            agent.SetDestination(location);
            actionsState = ActionState.Working;
        }
        else if(Vector3.Distance(agent.pathEndPosition,location)  >= 2) 
        {
            actionsState = ActionState.Idle;
            return Node.Status.Failure;
        }
        else if(distance < 2)
        {
            actionsState = ActionState.Idle;
            return Node.Status.Success;
        }
        return Node.Status.Running;
    }

    void Update()
    {
        if(treeStatus == Node.Status.Running) 
        {
            treeStatus = tree.Process();
        }
    }
}
