using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	private LineRenderer m_line;
    private int m_childCount;

	void Start () 
	{
		m_line = GetComponent<LineRenderer>();

		m_childCount = transform.childCount;
		m_line.positionCount = m_childCount;
	}
	
	void Update () {

		Vector3[] m_childPos = new Vector3[m_childCount];
		for (int i = 0; i < m_childCount; i++)
		{	
			if(transform.transform.GetChild(i).gameObject.GetComponent<CharacterJoint>() == null){
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
	}
}
