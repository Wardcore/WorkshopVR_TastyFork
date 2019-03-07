using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float speed;

	public bool rotate = true;
	void Update () {
		if(rotate)
			transform.Rotate(0,-speed,0);
	}
}
