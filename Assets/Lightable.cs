using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightable : MonoBehaviour {

	public bool isLighted = false;
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Flame" && !isLighted){
			print("Flame 1");
			isLighted = true;
			transform.GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}
}
