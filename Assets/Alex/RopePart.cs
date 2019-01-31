using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePart : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Scissors"){
			print("ropePart");
		}
	}

	public void BreakLink(){
		transform.parent.GetComponent<Rope>().Desac();
		Destroy(GetComponent<CharacterJoint>());
	}
}
