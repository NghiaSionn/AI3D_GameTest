using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Robber : BTController
{
    public GameObject diamond;
    public GameObject van;
    public GameObject monolisa;

    public List<GameObject> picture;

    public GameObject FrontDoor;
    public GameObject BackDoor;

  
    // Khởi động game ở trạng thái idle
    public ActionState actionsState = ActionState.Idle;

    // Trạng thái của cây
    Node.Status treeStatus = Node.Status.Running;

    [Range(0, 1000)]   public float money;

    void Start()
    {
        base.Start();

        //tạo các node
        var goToDiamond = new Leaf("Steal Diamond", GoToDiamond);
        var goToVan = new Leaf("Go to Van", GoToVan);
        var goToMonoLisa = new Leaf("Go to MonoLisa", GoToMonoLisa);
        //var goToPicture = new Leaf("Go to Picture", GoToPicture);
        var goToFrontDoor = new Leaf("Go to Front Door", GotToFrontDoor);
        var goToBackDoor = new Leaf("Go to Front Door", GotToBackDoor);

        var hasMoney = new Leaf ("Has Money", HasMoney);

        var openDoor = new Selector("Open Door");
        openDoor.AddChild(goToFrontDoor);
        openDoor.AddChild(goToBackDoor);

        var objectToSteal = new Selector("Object to Steal");
        objectToSteal.AddChild(goToDiamond);
        objectToSteal.AddChild(goToMonoLisa);
        
        var inverterMoney = new Inverter("Inverter Money");
        inverterMoney.AddChild(hasMoney);

        // tạo node Sequence
        var stealSomething = new Sequence("Steal something");
        stealSomething.AddChild(hasMoney);
        stealSomething.AddChild(openDoor);
        stealSomething.AddChild(goToMonoLisa);
        stealSomething.AddChild(goToDiamond);
        stealSomething.AddChild(goToVan);
        //stealSomething.AddChild(goToPicture);
        
        tree.AddChild(stealSomething);

        //in cây ra màn hình 
        tree.PrintTree(); 

        ////chạy cây
        //tree.Process();

    }

    private Node.Status GoToMonoLisa()
    {
        if (monolisa.activeSelf == false)  return Node.Status.Failure;
        var status = GoToLocation(monolisa.transform.position);
        if(status == Node.Status.Success) 
        {
            monolisa.transform.parent = this.gameObject.transform;
        }
        return status;
    }

    public Node.Status HasMoney()
    {
        if(money < 500)
        {
            return Node.Status.Failure;
        }
        return Node.Status.Success;
    }

    private Node.Status GoToDoor(GameObject door)
    {
        var status = GoToLocation(door.transform.position);
        if(status == Node.Status.Success)
        {
            if(!door.GetComponent<Lock>().islocked)
            {
                door.SetActive(false);
                return Node.Status.Success;
            }
            return Node.Status.Failure;
        }
        return status;
    }

    private Node.Status GotToBackDoor()
    {
        return GoToDoor(BackDoor);
    }

    private Node.Status GotToFrontDoor()
    {
        return GoToDoor(FrontDoor);
    }

    // Hàm xử lý go to van
    private Node.Status GoToVan()
    {
        var status = GoToLocation(van.transform.position);
        if(status == Node.Status.Success)
        {
            diamond.SetActive(false);
            monolisa.SetActive(false);
            money += 500;
        }
        return status;
    }

    // Hàm xử lý go to diamond
    private Node.Status GoToDiamond()
    {
        if (diamond.activeSelf == false)
            return Node.Status.Failure;
        var status = GoToLocation(diamond.transform.position);
        if (status == Node.Status.Success)
        {
            diamond.transform.parent = this.gameObject.transform;
        }

        return status;
    }

    // Hàm xử lý go to picture
    //private Node.Status GoToPicture()
    //{
    //    if (picture.Count == 0) return Node.Status.Failure;

    //    var randomIndex = UnityEngine.Random.Range(0, picture.Count);
    //    var targetPicture = picture[randomIndex];
    //    return GoToLocation(targetPicture.transform.position);
    //}
}
