using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightable : MonoBehaviour {

	public MeshRenderer m_LightableMesh;
	public Material m_LightedMat;
	public bool isLighted = false;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if(m_LightableMesh == null) 
			m_LightableMesh = GetComponent<MeshRenderer>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Flame" && !isLighted){
			isLighted = true;
			m_LightableMesh.material = m_LightedMat;
		}
	}
}
