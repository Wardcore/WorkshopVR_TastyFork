using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	public Material m_lineMat;
	public float m_lineSize = 0.02f;
	private LineRenderer m_line;
    public int m_childCount;
	
    public delegate void RopeCut(object sender);
	public event RopeCut RopeCutEvent;

	void  Awake() 
	{
		m_line = GetComponent<LineRenderer>();
		
		m_line.enabled = true;
		m_line.useWorldSpace = true;
		m_line.startWidth = m_lineSize;
		m_line.endWidth = m_lineSize;
		m_line.material = m_lineMat;
		//
		m_childCount = transform.childCount;
		m_line.positionCount = m_childCount;

		//Set ID
		for (int i = 0; i < m_childCount; i++)
		{		
			GameObject child = transform.GetChild(i).gameObject;
			//print("child : " + i + " = " + transform.GetChild(i).name);
			if(child.GetComponent<RopePart>() != null){
				child.GetComponent<CharacterJoint>().connectedBody = transform.GetChild(i-1).GetComponent<Rigidbody>();
				child.GetComponent<MeshRenderer>().enabled = false;
				child.GetComponent<RopePart>().ID = i;
			}
		}
	}
	
	void Update () {

		Vector3[] m_childPos = new Vector3[m_childCount];

		for (int i = 0; i < m_childCount; i++)
		{	
			if(transform.GetChild(i).gameObject.GetComponent<Joint>() == null){
				m_line.positionCount = i;
				break;
			}
			m_childPos[i] = transform.GetChild(i).position;
		}

		m_line.SetPositions(m_childPos);
	}

	public void Desac(){
		foreach (Transform child in transform)
		{
			child.GetComponent<Collider>().enabled = false;
		}
		OnRopeCut();
	}

	public virtual void OnRopeCut()
	{
		if (RopeCutEvent != null)
		{
			RopeCutEvent(this);
		}
	}

	public Vector3[] SetLine(int ID){
		Vector3[] TabPos = new Vector3[m_childCount - ID];

		//print("Tab length = " + TabPos.Length + ", ID = " + ID);
		for (int i = ID; i < m_childCount; i++)
		{
			TabPos[i - ID] =  transform.GetChild(i).position;
			//print("Pos " + i + " = " + TabPos[i - ID]);
		}
		return TabPos;
	}
}
