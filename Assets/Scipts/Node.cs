using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
	//các trạng thái của node
	public enum Status
	{
		Success,
		Failure,
		Running
	}

	//trạng thái của node
	public Status status;

	//các node con của node hiện tại
	public List<Node> children = new List<Node>();

	//con hiện tại
	public int currentChild = 0;

	//tên node
	public string name;

	// thứ tự
	public int sortOrder;

	//constructor
	public Node()
	{

	}

	public Node(string name)
	{
		this.name = name;
	}

	//thêm node con 
	public void AddChild(Node child) 
	{
		children.Add(child);
	}

	//hàm chạy node , trả về trạng thái của nodde
	public virtual Status Process()
	{
		return children[currentChild].Process();
	}
}
