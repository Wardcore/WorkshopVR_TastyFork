using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour {

	public bool m_IsPressed = false;
	public void ButtonPressed(){
		m_IsPressed = true;
	}
}
