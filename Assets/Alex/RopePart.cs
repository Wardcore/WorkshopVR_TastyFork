using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePart : MonoBehaviour {

	public int ID;
	private Rope m_parent;
	private LineRenderer lineTemp;
	private bool cut = false;

	void Start()
	{
		m_parent = transform.parent.GetComponent<Rope>();
	}

	void Update()
	{
		if(cut){
			lineTemp.SetPositions(m_parent.SetLine(ID));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Scissors" && !cut){
			BreakLink();
		}
	}

	public void BreakLink(){
		//print("Cut " + gameObject.name);
		//
		lineTemp = gameObject.AddComponent<LineRenderer>();
		lineTemp.positionCount = m_parent.m_childCount - ID;
		cut = true;
		//
		Destroy(GetComponent<CharacterJoint>());
		m_parent.Desac();
		//

		lineTemp.useWorldSpace = true;
		lineTemp.startWidth = m_parent.m_lineSize;
		lineTemp.endWidth = m_parent.m_lineSize;
		lineTemp.material = m_parent.m_lineMat;
		lineTemp.numCornerVertices = 90;

		//lineTemp.SetPositions(m_parent.SetLine(ID));
	}
}
