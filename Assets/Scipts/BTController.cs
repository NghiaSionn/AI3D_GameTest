using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BTController : MonoBehaviour
{
    public BehaviorTree tree;

    NavMeshAgent agent;

    // Trạng thái của nhân vật
    public enum ActionState
    {
        Idle,
        Working
    }

    // Khởi động game ở trạng thái idle
    public ActionState actionsState = ActionState.Idle;

    // Trạng thái của cây
    Node.Status treeStatus = Node.Status.Running;

    WaitForSeconds wait;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tree = new BehaviorTree();
        wait = new WaitForSeconds(UnityEngine.Random.Range(0.1f,1f));
        StartCoroutine("Behavior");
    }

    IEnumerator Behavior()
    {
        while(true)
        {
            treeStatus = tree.Process();
            yield return null;
        }

        yield return null;
    }
    
    // Hàm xử lý di chuyển đến vị trí 
    public Node.Status GoToLocation(Vector3 location)
    {
        var distance = Vector3.Distance(transform.position, location);
        if (actionsState == ActionState.Idle)
        {
            agent.SetDestination(location);
            actionsState = ActionState.Working;
        }
        else if (Vector3.Distance(agent.pathEndPosition, location) >= 2)
        {
            actionsState = ActionState.Idle;
            return Node.Status.Failure;
        }
        else if (distance < 2)
        {
            actionsState = ActionState.Idle;
            return Node.Status.Success;
        }
        return Node.Status.Running;
    }



    void Update()
    {
        if (treeStatus != Node.Status.Success)
        {
            treeStatus = tree.Process();
        }
    }
}
